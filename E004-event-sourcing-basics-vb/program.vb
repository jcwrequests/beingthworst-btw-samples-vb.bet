Module program

    Sub Main()
        Print("A new day at the factory starts...\r\n")
        Dim factory = New FactoryImplementation3()


        factory.TransferShipmentToCargoBay("chassis", {New CarPart("chassis", 4)})


        factory.AssignEmployeeToFactory("yoda")
        factory.AssignEmployeeToFactory("luke")
        '// Hmm, a duplicate employee name, wonder if that will work?
        factory.AssignEmployeeToFactory("yoda")
        '// An employee named "bender", why is that ringing a bell?
        factory.AssignEmployeeToFactory("bender")

        factory.TransferShipmentToCargoBay("model T spare parts",
                                           {New CarPart("wheels", 20),
                                            New CarPart("engine", 7),
                                            New CarPart("bits and pieces", 2)
                                            })


        Print("\r\nIt's the end of the day. Let's read our journal of events once more:\r\n")
        Print("\r\nWe should only see events below that were actually allowed to be recorded.\r\n")
        For Each e In factory.JournalOfFactoryEvents
            Print("!> {0}", e)
        Next

        Print("\r\nIt seems, this was an interesting day!  Two Yoda's there should be not!")
        Console.ReadLine()
    End Sub

    Sub Print(format As String, ParamArray args() As Object)

        If format.StartsWith("!") Then
            Console.ForegroundColor = ConsoleColor.DarkGreen
        ElseIf format.StartsWith("?") Then
            Console.ForegroundColor = ConsoleColor.DarkBlue
            Console.WriteLine()
        ElseIf format.StartsWith(" >") Then
            Console.ForegroundColor = ConsoleColor.DarkYellow
        Else
            Console.ForegroundColor = ConsoleColor.Gray
        End If

        Console.WriteLine(format, args)

    End Sub

    Sub Fail(format As String, ParamArray args() As Object)

        Console.ForegroundColor = ConsoleColor.DarkRed
        Console.WriteLine(format, args)

    End Sub

End Module
