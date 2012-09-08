Imports System.Runtime.Serialization.Formatters.Binary
Imports System.IO

Public Class SimpleNetSerializer
    ReadOnly _formatter As New BinaryFormatter

    Public Sub WriteMessage(message As Object, type As Type, stream As Stream)
        _formatter.Serialize(stream, message)
    End Sub
    Public Function ReadMessaege(stream As Stream) As Object
        Return _formatter.Deserialize(stream)
    End Function
End Class
