#include <gtest/gtest.h>
#include <sales_goods.h>

class SalesGoodsTest : public ::testing::Test {
  public:
    virtual void SetUp() {
    }

    virtual void TearDown() {
    }
  protected:
    SalesGoods goods;
};

TEST_F( SalesGoodsTest, AddSpecifiedGoodsName ){
    SalesGoods goods( "cola", Money(120) );
    ASSERT_EQ( goods.get_name(), "cola" );
}

/*
TEST_F( SalesGoodsTest, AddSpecifiedGoodsPrice ){
    SalesGoods goods( "cola", Money(120) );
    ASSERT_EQ( goods.get_price(), 120 );
}
*/
