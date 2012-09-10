Public Class ShipmentTransferredToCargoBay
    Implements IEvent
    Public ShipmentName As String
    Public CarParts() As CarPart

    Public Overrides Function ToString() As String
        Dim builder As New Text.StringBuilder
        builder.AppendFormat("Shipement '{0}' transferred to cargo bay:", ShipmentName).AppendLine()
        For Each carPart In CarParts
            builder.AppendFormat("      {0} {1} pcs", carPart.Name, carPart.Quantity).AppendLine()
        Next
        Return builder.ToString
    End Function

End Class
Public Class CurseWordUttered
    Implements IEvent
    Public TheWord As String
    Public Meaning As String

    Public Overrides Function ToString() As String
        Return String.Format("'{0}' was heard with the walls.  It meant:\r\n     '{1}'", TheWord, Meaning)

    End Function
End Class
Public Class EmployeeAssignedToFactory
    Implements IEvent
    Public EmployeName As String
    Public Overrides Function ToString() As String
        Return String.Format("new worked joins our forces:  '{0}'", EmployeName)
    End Function
End Class
