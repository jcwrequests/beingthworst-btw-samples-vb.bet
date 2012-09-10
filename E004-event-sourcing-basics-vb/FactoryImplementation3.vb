Public Class FactoryImplementation3
    Public JournalOfFactoryEvents As New List(Of IEvent)
    ReadOnly _ourListOfEmployeeNames As New List(Of String)
    ReadOnly _shipmentsWaitingToBeUnloaded As New List(Of CarPart())

    Public Sub AssignEmployeeToFactory(employeeName As String)
        Print("?> Command:  Assign employee {0} to factory", employeeName)
        If _ourListOfEmployeeNames.Contains(employeeName) Then
            Fail(":> the name of '{0}' only one employee can have", employeeName)
            Return

        End If
        If employeeName.Equals("bender") Then
            Fail(":> Guys with the name 'bender' are trouble.")
            Return
        End If
        DoPaperWork("Assign employy to the factory")
        RecordThat(New EmployeeAssignedToFactory With {.EmployeName = employeeName})

    End Sub
    Public Sub TransferShipmentToCargoBay(shipentName As String, parts() As CarPart)
        Print("?> Command: transfer shipment to cargo bay")
        If _ourListOfEmployeeNames.Count.Equals(0) Then
            Fail(":> There has to be somebody at factory in order to accept shipment")
            Return
        End If
        If _shipmentsWaitingToBeUnloaded.Count > 2 Then
            Fail(":> More than two shipments can't fit into this cargo bay :(")
            Return
        End If
        DoRealWork("opening cargo bay doors")
        RecordThat(New ShipmentTransferredToCargoBay() With {.ShipmentName = shipentName,
                                                             .CarParts = parts})
        Dim totalCountOfParts = parts.Sum(Function(p) p.Quantity)
        If totalCountOfParts > 10 Then
            RecordThat(New CurseWordUttered With {.TheWord = "Boltov tebe v korobky peredach",
                                                  .Meaning = "awe in the face of the amount of parts delivered"})

        End If

    End Sub
    Sub DoPaperWork(workName As String)
        Print(" > Work:  papers... {0}...", workName)
        System.Threading.Thread.Sleep(1000)
    End Sub
    Sub DoRealWork(workName As String)
        Print(" > Work:  heavy stuff.. {0}...", workName)
        Threading.Thread.Sleep(1000)
    End Sub
    Public Sub RecordThat(e As IEvent)
        JournalOfFactoryEvents.Add(e)
        Dim _event As Object = e
        Me.AnnouceInsideFactory(_event)
    End Sub
    Public Sub AnnouceInsideFactory(e As EmployeeAssignedToFactory)
        _ourListOfEmployeeNames.Add(e.EmployeName)
    End Sub
    Public Sub AnnouceInsideFactory(e As ShipmentTransferredToCargoBay)
        _shipmentsWaitingToBeUnloaded.Add(e.CarParts)
    End Sub
    Public Sub AnnouceInsideFactory(e As CurseWordUttered)

    End Sub
End Class
