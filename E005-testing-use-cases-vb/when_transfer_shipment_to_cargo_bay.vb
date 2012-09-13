Imports System
Imports System.Reflection
Imports NUnit.Framework

Public NotInheritable Class when_transfer_shipment_to_cargo_bay
    Inherits factory_specs

    <Test>
    Public Sub empty_shipment()
        Dim Given() As IEvent = {New EmployeeAssignedToFactory("yoda")}

        [When] = Sub(f) f.TransferShipmentToCargoBay("some shipment", {New CarPart("chassis", 1)})
        TheException = Function(ex) ex.Message.Contains("has to be somebody at factory")

    End Sub
    <Test>
    Public Sub there_already_are_two_shipments()
        Given = {New EmployeeAssignedToFactory("chubakka"),
                 New ShipmentTransferredToCargoBay("shipmt-11", New CarPart("engine", 3)),
                 New ShipmentTransferredToCargoBay("shipmt-12", New CarPart("wheels", 40))}
        [When] = Sub(f) f.TransferShipmentToCargoBay("shipmt-11", New CarPart("bmw6", 20))
        TheException = Function(ex) ex.Message.Contains("More than two shipments can't fit")

    End Sub
End Class
