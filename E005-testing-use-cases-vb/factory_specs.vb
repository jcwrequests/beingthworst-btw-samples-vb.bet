Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Runtime.Serialization.Formatters.Binary
Imports NUnit.Framework

<TestFixture>
Public Class factory_specs
    Public Given As IList(Of IEvent)
    Public [When] As Expression(Of Action(Of FactoryAggregate))
    Public WriteOnly Property [Then] As IList(Of IEvent)
        Set(value As IList(Of IEvent))
            AssertCustomerGWT(Given, [When], value)
        End Set
    End Property
    Public WriteOnly Property TheException As Expression(Of Predicate(Of Exception))
        Set(value As Expression(Of Predicate(Of Exception)))
            Try
                Execute(Given, [When])
                Assert.Fail(String.Format("Expected exception: {0}", value))
            Catch ex As Exception
                Console.WriteLine("Then expect exception: " + value.ToString)
                If Not value.Compile()(ex) Then Throw ex
            End Try

        End Set
    End Property
    <SetUp>
    Public Sub Setup()
        Given = NoEvents
        [When] = Nothing
    End Sub
    Public ReadOnly NoEvents() As IEvent = {}

    Shared Sub AssertCustomerGWT(given As ICollection(Of IEvent),
                                 [when] As Expression(Of Action(Of FactoryAggregate)),
                                 [then] As ICollection(Of IEvent))
        Dim changes = Execute(given, [when])

        If [then].Count.Equals(0) Then
            Console.Write("Expect no events")
        Else
            For Each [event] In [then]
                Console.WriteLine("Then:  " + [event].ToString)
            Next
        End If
        AssertEquality([then].ToArray, changes.toarray)
    End Sub
    Shared Function Execute(given As ICollection(Of IEvent),
                            [when] As Expression(Of Action(Of FactoryAggregate)))
        If [given].Count.Equals(0) Then Console.WriteLine("Given no events")
        For Each [event] In given
            Console.WriteLine("Given:  " + [event].ToString)
        Next
        Dim customer As New FactoryAggregate(New FactoryState(given))
        PrintWhen([when]:=[when])

        [when].Compile()(customer)
        Return customer.Changes

        PrintWhen([when])

    End Function
    Shared Sub PrintWhen([when] As Expression(Of Action(Of FactoryAggregate)))
        Console.WriteLine()
        Console.WriteLine("When: " + [when].ToString)
        Console.WriteLine()
    End Sub
    Shared Sub AssertEquality(expected() As IEvent, actual() As IEvent)
        Dim actualBytes = SerializeEventsToBytes(actual)
        Dim expectedBytes = SerializeEventsToBytes(expected)
        Dim areEqual As Boolean = actualBytes.SequenceEqual(expectedBytes)
        If areEqual Then Return

        CollectionAssert.AreEqual(expected.Select(Function(s) s.ToString.ToArray),
                                  actual.Select(Function(s) s.ToString.ToArray))
        CollectionAssert.AreEqual(expectedBytes, actualBytes,
                                  "Expected events differ from actual, but differences are not represented in ToString()")


    End Sub
    Shared Function SerializeEventsToBytes(actual() As IEvent) As Byte()
        Dim formatter As New BinaryFormatter
        Using mem As New MemoryStream
            formatter.Serialize(mem, actual)
            Return mem.ToArray
        End Using
    End Function

End Class
