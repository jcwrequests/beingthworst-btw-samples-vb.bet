Module program

    Sub Main()
        RunSpecification(New when_assign_employee_to_factory)
        RunSpecification(New when_transfer_shipment_to_cargo_bay)
        Console.ReadLine()
    End Sub
    Public Sub RunSpecification(specification As factory_specs)
        Console.WriteLine(New String("=", 80))
        Dim cases = specification.GetType.GetMethods(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance Or Reflection.BindingFlags.DeclaredOnly)
        Print(ConsoleColor.DarkGreen, "Specification:  {0}", specification.GetType.Name.Replace("_", " "))
        For Each methodInfo In cases
            Console.WriteLine(New String("-", 80))
            Print(ConsoleColor.DarkBlue, "User case:  {0}", methodInfo.Name.Replace("_", " "))
            Console.WriteLine()
            Try
                specification.Setup()
                methodInfo.Invoke(specification, Nothing)
                Print(ConsoleColor.DarkGreen, "\r\n\PASSED!")
            Catch ex As Exception
                Print(ConsoleColor.DarkRed, "\r\nFAIL!")
            End Try
        Next


    End Sub
    Sub Print(color As ConsoleColor, format As String, ParamArray args() As Object)
        Dim old = Console.ForegroundColor
        Console.ForegroundColor = color
        Console.WriteLine(format, args)
        Console.ForegroundColor = old

    End Sub
End Module
