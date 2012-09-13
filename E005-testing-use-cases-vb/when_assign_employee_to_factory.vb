Imports System
Imports System.Reflection
Imports NUnit.Framework

Public NotInheritable Class when_assign_employee_to_factory
    Inherits factory_specs

    <Test>
    Public Sub empty_factory()
        [When] = Sub(f) f.AssignEmployeeToFactory("fry")
        [Then] = {New EmployeeAssignedToFactory("fry")}

    End Sub
    <Test>
    Public Sub fry_is_assigned_to_factory()
        Given = {New EmployeeAssignedToFactory("fry")}
        [When] = Sub(f) f.AssignEmployeeToFactory("fry")
        TheException = Function(ex) ex.Message.Contains("only one employee can have")

    End Sub
    <Test>
    Public Sub bender_comes_to_empty_factory()
        [When] = Sub(f) f.AssignEmployeeToFactory("bender")
        TheException = Function(ex) ex.Message.Contains("Guys with name 'bender' are trouble")

    End Sub
End Class
