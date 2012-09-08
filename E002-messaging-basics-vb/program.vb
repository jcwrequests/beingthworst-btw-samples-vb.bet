Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Text

Module program

    Sub Main(args() As String)

        Print(File.ReadAllText("Readme.md"))


        '/ Note:  You can push Ctrl+F5 to run this program sample and see the console output
        ' Our goal is to allow customers to add & remove shopping items to their product basket
        '// so that they can checkout and buy whatever is in the basket when they are done shopping.
        '// In the sample below, we will show two possible approaches for achieving that goal:
        '// 1)  The traditional approach of calling methods on objects direclty
        '// 2)  The messaging approach using message classes that contains the data the remote method needs

        '// Note: "Comment" is just a small utility method that helps us write text to the console window
        Dim msg = <text>
            Let's create a new product basket to hold our shopping items and simply
            add some products to it directly via traditonal BLOCKING method calls.
                  </text>
        Print(msg.Value)
        Dim basket = New ProductBasket()

        ' Add some products to that shopping basket
        basket.AddProduct("butter", 1)
        basket.AddProduct("pepper", 2)

        msg = <text>
                    Now, to add more stuff to the shopping basket via messaging (instead of a
                    direct method call), we create an AddProductToBasketMessage to store our name
                    and quantity arguments that will be provided to ProductBasket.AddProduct later
              </text>
        Print(msg.Value)

        Dim message As New AddProductToBasktMessage("candles", 5)

        msg = <text>
                  Now, since we created that message, we will apply its item contents of: {0}
                  by sending it to the product basket to be handled.
              </text>
        Print(String.Format(msg.Value, message))

        ApplyMessage(basket, message)

        msg = <text>
                  We don't have to send/apply messages immediately.  We can put messages into 
                  some queue and send them later if needed. 

                 Let's define more messages to put in a queue:
              </text>
        Dim queue = New Queue(Of Object)
        queue.Enqueue(New AddProductToBasktMessage("Chablis wine", 1))
        queue.Enqueue(New AddProductToBasktMessage("shrimps", 10))


        For Each enqueuedMessage In queue
            Print(String.Format("[Message in Queue is:]  {0}", enqueuedMessage))
        Next

        msg = <text>
            This is what temporal decoupling is. Our product basket does not 
            need to be available at the same time that we create and memorize
            our messages. This will be extremely important, when we get to 
            building systems that balance load and can deal with failures.

            Now that we feel like it, let's send our messages that we put in the
            queue to the ProductBasket:
              </text>
        Print(msg.Value)

        While queue.Count > 0
            ApplyMessage(basket, queue.Dequeue)
        End While

        msg = <text>
                  Now let's serialize our message to binary form,
                  which allows the message object to travel between processes.
              </text>


        Print(msg.Value)
        Dim sertializer As New SimpleNetSerializer

        msg = <text>
            Serialization is a process of recording an object instance
            (which currenly only exists in RAM/memory)
            to a binary representation (which is a set of bytes).
            Serialization is a way that we can save the state of our
            object instances to persistent (non-memory) storage.


            The code will now create another new message for the 'rosmary' product,
            but this time it will serialize it from RAM to disk.
              </text>

        Print(msg.Value)

        Dim newMsg As New AddProductToBasktMessage("rosemary", 1)


        Dim bytes() As Byte
        Using stream As New IO.MemoryStream
            sertializer.WriteMessage(newMsg, newMsg.GetType(), stream)
            bytes = stream.ToArray
        End Using

        msg = <text>
                  Let's see how this 'rosmary' message object would look in its binary form:
              </text>
        Print(msg.Value)

        Console.WriteLine(BitConverter.ToString(bytes).Replace("-", ""))

        msg = <text>
                  And if we tried to open it in a text editor...
              </text>

        Print(msg.Value)
        Console.WriteLine(Encoding.ASCII.GetString(bytes))

        msg = <text>
                   Note the readable string content with some 'garbled' binary data!
            Now we'll save (persist) the 'rosmary' message to disk, in file 'message.bin'.
                
            You can see the message.bin file inside of:

           {0}

            If you open it with Notepad, you will see the 'rosmary' message waiting on disk for you.

              </text>
        Print(String.Format(msg.Value, Path.GetFullPath("message.bin")))

        File.WriteAllBytes("message.bin", bytes)

        msg = <text>
            Let's read the 'rosmary' message we serialized to file 'message.bin' back into memory.

            The process of reading a serialized object from byte array back into intance in memory 
            is calld deserialization.
              </text>
        Print(msg.Value)

        Using stream = File.OpenRead("message.bin")
            Dim readMessage = sertializer.ReadMessaege(stream)
            msg = <text>
                      "[Serialized Message was read from disk:] {0}
                  </text>
            Print(String.Format(msg.Value, readMessage))
            msg = <text>Now let's apply that messaage to the product basket.</text>
            Print(msg.Value)
            ApplyMessage(basket, readMessage)
        End Using

        msg = <text>
            Now you've learned what a message is (just a remote temporally
            decoupled message/method call, that can be persisted and then
            dispatched to the place that handles the request.

            You also learned how to actually serialize a message to a binary form
            and then deserialize it and dispatch it the handler.

            As you can see, you can use messages for passing information
            between machines, telling a story and also persisting.
            
            By the way, let's see what we have aggregated in our product basket so far:
              </text>
        Print(msg.Value)

        For Each total In basket.GetProductTotals()
            Console.WriteLine("  {0}: {1}", total.Key, total.Value)
        Next

        msg = <text>
            And that is the basics of messaging!

            Stay tuned for more episodes and samples!


            # Home work assignment.

            * For C# developers - implement 'RemoveProductFromBasket'
            * For non-C# developers - implement this code in your favorite platform.

            NB: Don't hesitate to ask questions, if you get any.
              </text>

        Print(msg.Value)
        Console.ReadLine()

    End Sub

    Public Sub ApplyMessage(basket As ProductBasket, message As Object)
        basket.When(message)
    End Sub
    Sub Print(message As String)
        Dim oldColor = Console.ForegroundColor
        For Each line In message.Split({Environment.NewLine}, StringSplitOptions.None)
            Dim trimmed = line.TrimStart
            If trimmed.StartsWith("#") Then
                Console.ForegroundColor = ConsoleColor.DarkRed
            ElseIf trimmed.StartsWith("*") Or trimmed.StartsWith("- ") Then
                Console.ForegroundColor = ConsoleColor.DarkBlue
            Else
                Console.ForegroundColor = ConsoleColor.DarkGreen
            End If

            Console.WriteLine(trimmed)
            Console.ForegroundColor = oldColor
        Next

    End Sub
End Module
Public Class ProductBasket
    ReadOnly _products As New Dictionary(Of String, Double)

    
    Public Function GetProductTotals() As IDictionary(Of String, Double)
        Return _products
    End Function
   
    Public Sub AddProduct(name As String, quantity As Double)
        Dim currenyQuantity As Double
        If Not _products.TryGetValue(name, currenyQuantity) Then
            currenyQuantity = 0
        End If
        If Not _products.ContainsKey(name) Then _products.Add(name, 0)
        _products(name) = currenyQuantity + quantity

        Console.WriteLine("Shopping Basket said:  I added {0} unit(s) if '{1}'", quantity, name)

    End Sub
    Public Sub [When](toBasketMessage As AddProductToBasktMessage)
        Console.WriteLine("[Message Applied]")
        AddProduct(toBasketMessage.Name, toBasketMessage.Quantity)

    End Sub
End Class
<Serializable>
Public Class AddProductToBasktMessage
    Public ReadOnly Name As String
    Public ReadOnly Quantity As Double
    Public Sub New(name As String, quantity As Double)
        Me.Name = name
        Me.Quantity = quantity

    End Sub
    
End Class