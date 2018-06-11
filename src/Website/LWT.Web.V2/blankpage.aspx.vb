Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Drawing
Imports System.Net.Mail
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Util


Partial Class blankpage
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        '====================== LetterBillingToDealer ==================================
        Dim pdfTemplate As String = "C:\Temp\Template.pdf"
        Dim newPdfFile As String = "C:\Temp\newTemplate.pdf"
        Dim LogoImage As String = "C:\Temp\LWTLogo.jpg"
        Dim pdfReader As New PdfReader(pdfTemplate)
        Dim pdfStamper As New PdfStamper(pdfReader, New FileStream(newPdfFile, FileMode.Create))
        Dim imgWidth As Integer = 600
        Dim imgHeight As Integer = 70

        Dim NumberOfPages As Integer = pdfReader.NumberOfPages
        For pageno = 2 To NumberOfPages
            Dim overContent As PdfContentByte = pdfStamper.GetOverContent(pageno)
            Dim Img = iTextSharp.text.Image.GetInstance(LogoImage)
            'Img.SetAbsolutePosition(overContent.PdfDocument.PageSize.Right - 160, overContent.PdfDocument.PageSize.Top - 80)
            Img.SetAbsolutePosition((overContent.PdfDocument.PageSize.Width / 2) - 50, overContent.PdfDocument.PageSize.Top - 80)

            Img.ScaleToFit(imgWidth, imgHeight)
            overContent.AddImage(Img)
        Next
        pdfStamper.FormFlattening = False
        pdfStamper.FreeTextFlattening = True
        pdfStamper.Close()
        pdfReader.Close()
        '==================== Top Center =================
        'Dim overContent1 As PdfContentByte = pdfStamper.GetOverContent(pageno)
        'Dim pagerec1 As iTextSharp.text.Rectangle = pdfReader.GetCropBox(pageno)
        'Dim Img1 = iTextSharp.text.Image.GetInstance(LogoImage)
        'Dim marginLR As Single = 36
        'Dim marginB As Single = 2
        'Dim footerHeight As Single = 10
        ''Dim rect As New Rectangle(pagerec1.Left + marginLR, pagerec1.Top + marginB, pagerec1.Right - marginLR, pagerec1.Top + marginB + footerHeight)
        'Img1.SetAbsolutePosition((overContent1.PdfDocument.PageSize.Width / 2) - 50, overContent1.PdfDocument.PageSize.Top - 80)
        'Img1.ScaleToFit(600, 70)
        'overContent1.AddImage(Img1)

     
        '==================== Top Right =================
        'pageno = 2
        'Dim overContent2 As PdfContentByte = pdfStamper.GetOverContent(pageno)
        'Dim pagerec2 As iTextSharp.text.Rectangle = pdfReader.GetCropBox(pageno)
        'Dim Img2 = iTextSharp.text.Image.GetInstance(LogoImage)

        ''Dim rect2 As New Rectangle(pagerec2.Left + marginLR, pagerec2.Top + marginB, pagerec2.Right - marginLR, pagerec2.Top + marginB + footerHeight)
        'Img2.SetAbsolutePosition(overContent2.PdfDocument.PageSize.Right - 160, overContent2.PdfDocument.PageSize.Top - 80)
        'Img2.ScaleToFit(600, 70)
        'overContent2.AddImage(Img2)


      
        ''========================================================


        ' ''//File that we are creating
        'Dim OutputFile As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test.pdf")
        ' ''//Image to place
        'Dim SampleImage As String = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "SampleImage.jpg")

        ' ''//Standard PDF creation setup
        'Using FS As New FileStream(OutputFile, FileMode.Open, FileAccess.Write, FileShare.None)
        '    Using Doc As New Document(PageSize.A4)
        '        Using writer = PdfWriter.GetInstance(Doc, FS)

        '            ''//Open the document for writing
        '            Doc.Open()
        '            ''//Add a simple paragraph
        '            Doc.Add(New Paragraph("Hello world"))

        '            ''//Create an image object
        '            Dim Img = iTextSharp.text.Image.GetInstance(SampleImage)
        '            ''//Give it an absolute position in the top left corner of the document (remembering that 0,0 is bottom left, not top left)
        '            Img.SetAbsolutePosition(0, Doc.PageSize.Height - Img.Height)
        '            ''//Add it directly to the raw pdfwriter instead of the document helper. DirectContent is above and DirectContentUnder is below
        '            writer.DirectContent.AddImage(Img)



        '            ''//Close the document
        '            Doc.Close()
        '        End Using
        '    End Using
        'End Using

    End Sub




End Class
