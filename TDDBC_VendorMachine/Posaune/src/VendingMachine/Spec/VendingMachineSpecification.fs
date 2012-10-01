module Spec.VendingMachineSpecification

open NaturalSpec

open VendingMachine

let sut = new VendingMachine();

let get_total_amount (vm:VendingMachine) =
    printMethod ()
    vm.TotalAmount 
    
//Feature:�������z�̊m�F���ł���
[<Scenario>]
let ``Initially, the total amount of this vending machine is 0``() =
  Given sut              
    |> When get_total_amount      
    |> It should equal 0
    |> Verify
                 