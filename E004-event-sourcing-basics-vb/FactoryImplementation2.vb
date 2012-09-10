Public NotInheritable Class FactoryImplementation2


    Public Sub AssignEmployeeToFactory(employeeName As String)
        '// CheckIfEmployeeCanBeAssignedToFactory(employeeName);
        '// DoPaperWork();
        '// RecordThatEmployeeAssignedToFactory(employeeName);
    End Sub
    Public Sub TransferShipmentToCargoBay(shipentName As String, parts() As CarPart)
        '// CheckIfCargoBayHasFreeSpace(parts);
        '// DoRealWork("unloading supplies...");
        '// DoPaperWork("Signing the shipment acceptance form");
        '// RecordThatSuppliesAreAvailableInCargoBay()
    End Sub
    Public Sub UnloadShipmentFromCargoBay(employeeName As String)
        '// DoRealWork("passing supplies");
        '// RecordThatSuppliesWereUnloadedFromCargoBay()
    End Sub

    Public Sub ProduceCar(employeeName As String, carModel As String)
        '// CheckIfWeHaveEnoughSpareParts
        '// CheckIfEmployeeIsAvailable
        '// DoRealWork
        '// RecordThatCarWasProduced
    End Sub

End Class
