module Spec.VendingMachineSpecification

open NaturalSpec

open VendingMachine

let initially vm =
    vm

let total_amount amount (vm:VendingMachine)  =
    printMethod amount 
    vm.TotalAmount = amount 

let insert_money amount (vm:VendingMachine) = 
    printMethod amount
    amount |> List.iter vm.InsertMoeny
    vm
 
//Feature:�����������ł���
[<Scenario>]
let ``After inserting 10 yen, it's total amount is 10``() =
  Given (new VendingMachine())          
    |> When insert_money [10]      
    |> It should have (total_amount 10)
    |> Verify

[<Scenario>]
let ``After inserting 10 yen and 100 yen, it's total amount is 110``() =
  Given (new VendingMachine())          
    |> When insert_money [10;100]  
    |> It should have (total_amount 110)
    |> Verify
                 
   
//Feature:�������z�̊m�F���ł���
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given (new VendingMachine())                
    |> When initially      
    |> It should have (total_amount 0)
    |> Verify


//Feature:�����Ȃ��������Ǘ��ł���
[<Scenario>]
let ``After inserting 1 yen, it's invalid so machine's total amout is 0``() =
  Given (new VendingMachine())                
    |> When insert_money [1]      
    |> It should have (total_amount 0)
    |> Verify
    

