Public NotInheritable Class FactoryImplementation1
    ' // the methods below are linguistically equivalent to a command message 
    '// that could be sent to this factory. A command such as:
    '// public class AssignEmployeeToFactory
    '// {
    '//    public string EmployeeName { get; set; }
    '// }

    '// in this sample we will not create command messages to represent 
    '// and call these methods, we will just use the methods themselves to be our
    '// "commands" for convenience.

    Public Sub AssignEmployeeToFactory(employeeName As String)

    End Sub
    Public Sub TransferShipmentToCargoBay(shipmentName As String, parts() As CarPart)

    End Sub
    Public Sub UnloadShipmentFromCargoBay(employeeName As String)

    End Sub
    Public Sub ProduceCar(employeeName As String, carModel As String)

    End Sub
End Class
