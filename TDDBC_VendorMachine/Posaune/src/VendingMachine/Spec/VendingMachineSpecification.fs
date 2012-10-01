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
    vm.InsertMoeny(amount)
    vm
 
//Feature:�����������ł���
let sut1 = new VendingMachine();
[<Scenario>]
let ``After inserting 10 yen, it's total amount is 10``() =
  Given (new VendingMachine())          
    |> When insert_money 10      
    |> It should have (total_amount 10)
    |> Verify
                 
   
//Feature:�������z�̊m�F���ł���
let sut2 = new VendingMachine();
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given (new VendingMachine())                
    |> When initially      
    |> It should have (total_amount 0)
    |> Verify


    

