using System;
using System.Linq;
using System.Collections.Generic;

using VendingMachine.Model;

using NUnit.Framework;

namespace VendingMachine.Test {
    [TestFixture]
    public class _商品購入に関するTestSuite {
        public class 商品選択状態の変化Params {
            public class Param {
                public Money[] Credits {get; internal set;}
                public ItemRackState[] States {get; internal set;}
            }

            public IEnumerable<Param> Source {
                get {
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanNotPurchase, ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.CanNotPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.CanPurchase),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.CanPurchase, ItemRackState.CanPurchase),
                    };
                }
            }

            public IEnumerable<Param> SoldOutSource {
                get {
                    // Sold OUT !!!
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                    yield return new Param {
                        Credits = TestHelper.AsArray(Money.Coin100, Money.Coin100, Money.Coin100, Money.Coin100),
                        States = TestHelper.AsArray(ItemRackState.Soldout, ItemRackState.Soldout),
                    };
                }
            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化(
            [ValueSource(typeof(商品選択状態の変化Params), "Source")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var racks = this.InitItemRack(ItemRackState.CanNotPurchase);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashFlow();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }
            
            foreach (var result in racks.Items.Zip(inParameter.States, (r, s) => Tuple.Create(r, s))) {
                itemRackRole.UpdateItemSelectionState(result.Item1, credit, pool);
                
                Assert.That(result.Item1.SelectionState, Is.EqualTo(result.Item2));
            }
        }

        [Test]
        public void _お金投入による商品選択状態の変化_売り切れの場合(
            [ValueSource(typeof(商品選択状態の変化Params), "SoldOutSource")] 
            商品選択状態の変化Params.Param inParameter) 
        { 
            var racks = this.InitItemRack(ItemRackState.Soldout);
            
            var pool = TestHelper.InitInfinityReservedChange();
            var credit = new CashFlow();
            
            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }
            
            foreach (var result in racks.Items.Zip(inParameter.States, (r, s) => Tuple.Create(r, s))) {
                itemRackRole.UpdateItemSelectionState(result.Item1, credit, pool);
                
                Assert.That(result.Item1.SelectionState, Is.EqualTo(result.Item2));
            }
        }

        [Test]
        public void _指定された位置の商品ラックを検索する() {
            var racks = this.InitItemRack(ItemRackState.CanNotPurchase);
            
            var itemRackRole = new  ItemRackRole();
            var rack = itemRackRole.FindRackAt(racks, 0);
            Assert.That(rack, Is.Not.Null);
            Assert.That(rack.Item.Name, Is.EqualTo("Item0"));
        }
        
        [Test]
        public void _指定された位置の商品ラックを検索する_未配置の位置を指定する場合() {
            var racks = this.InitItemRack(ItemRackState.CanNotPurchase);
            
            var itemRackRole = new  ItemRackRole();
            var rack = itemRackRole.FindRackAt(racks, 3);
            Assert.That(rack, Is.Null);
        }

        [Test]
        public void _商品ラック配置のラックコレクションは位置順に並んでいる() {
            var racks = this.InitItemRack(ItemRackState.CanNotPurchase);

            var keys = racks.Positions.Keys.OrderBy(k => k);
            foreach (var pair in keys.Zip(racks.Items, (n, rack) => Tuple.Create(n, rack))) {
                Assert.That(racks.Positions[pair.Item1], Is.EqualTo(pair.Item2));
            }
        }

        private ItemRackPosition InitItemRack(ItemRackState inState) {
            return new ItemRackPosition(
                Tuple.Create(
                    0, 
                    new ItemRack {
                        Item = new Item { Name = "Item0", Price = 120,  Shape = ItemShapeType.Can350 },    
                        SelectionState = inState,
                    }
                ),
                Tuple.Create(
                    1, 
                    new ItemRack {
                        Item = new Item { Name = "Item1", Price = 250,  Shape = ItemShapeType.Can500 },
                        SelectionState = inState,
                    }
                )
            );
        }

        [Test]
        public void _指定された位置の商品が選択可能かどうか(
                [ValueSource(typeof(商品選択状態の変化Params), "Source")] 
                商品選択状態の変化Params.Param inParameter) 
        {
            var racks = this.InitItemRack(ItemRackState.CanNotPurchase);
            
            var credit = new CashFlow();
            var pool = TestHelper.InitInfinityReservedChange();

            var coinMeckRole = new CoinMeckRole();
            var itemRackRole = new  ItemRackRole();
            
            foreach (var c in inParameter.Credits) {
                coinMeckRole.Receive(credit, c);
            }

            var fixtures = racks.Positions.Keys
                .OrderBy(k => k)
                .Zip(inParameter.States, (k, s) => {
                    return Tuple.Create(k, s == ItemRackState.CanPurchase);
                })
            ;

            foreach (var fixture in fixtures) {
                var rack = itemRackRole.FindRackAt(racks, fixture.Item1);
                itemRackRole.UpdateItemSelectionState(rack, credit, pool);
                
                Assert.That(itemRackRole.CanItemPurchase(racks, fixture.Item1), Is.EqualTo(fixture.Item2));
            }
        }
    }
}

