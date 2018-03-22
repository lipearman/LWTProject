Imports Microsoft.VisualBasic

Namespace Context

   
    <Serializable()> _
    Public Class Employee
        Private _EmployeeID As Object
        Private _LastName As Object
        Private _FirstName As Object
        Private _Title As Object
        Private _TitleOfCourtesy As Object
        Private _BirthDate As Object
        Private _HireDate As Object
        Private _Address As Object
        Private _City As Object
        Private _Region As Object
        Private _PostalCode As Object
        Private _Country As Object
        Private _HomePhone As Object
        Private _Extension As Object
        Private _Photo As Object
        Private _Notes As Object
        Private _ReportsTo As Object
        Private _PhotoPath As Object
        Private _UserID As Object
        Private _EmpGroupID As Object
        Private _Email As Object
        Private _Province As Object
        Private _Amphur As Object
        Private _District As Object
        Private _LOCATION_CODE As Object
        Private _TempUserName As Object
        Private _TempPassword As Object
        Private _TempPasswordQuestion As Object
        Private _TempPasswordAnswer As Object
        Private _IsActivate As Object
        Private _ActivateCode As Object
        Private _ActivationDate As Object


        Public Property EmployeeID() As Object
            Get
                Return _EmployeeID
            End Get
            Set(ByVal value As Object)
                _EmployeeID = value
            End Set
        End Property

        Public Property LastName() As Object
            Get
                Return _LastName
            End Get
            Set(ByVal value As Object)
                _LastName = value
            End Set
        End Property

        Public Property FirstName() As Object
            Get
                Return _FirstName
            End Get
            Set(ByVal value As Object)
                _FirstName = value
            End Set
        End Property

        Public Property Title() As Object
            Get
                Return _Title
            End Get
            Set(ByVal value As Object)
                _Title = value
            End Set
        End Property

        Public Property TitleOfCourtesy() As Object
            Get
                Return _TitleOfCourtesy
            End Get
            Set(ByVal value As Object)
                _TitleOfCourtesy = value
            End Set
        End Property

        Public Property BirthDate() As Object
            Get
                Return _BirthDate
            End Get
            Set(ByVal value As Object)
                _BirthDate = value
            End Set
        End Property

        Public Property HireDate() As Object
            Get
                Return _HireDate
            End Get
            Set(ByVal value As Object)
                _HireDate = value
            End Set
        End Property

        Public Property Address() As Object
            Get
                Return _Address
            End Get
            Set(ByVal value As Object)
                _Address = value
            End Set
        End Property

        Public Property City() As Object
            Get
                Return _City
            End Get
            Set(ByVal value As Object)
                _City = value
            End Set
        End Property

        Public Property Region() As Object
            Get
                Return _Region
            End Get
            Set(ByVal value As Object)
                _Region = value
            End Set
        End Property

        Public Property PostalCode() As Object
            Get
                Return _PostalCode
            End Get
            Set(ByVal value As Object)
                _PostalCode = value
            End Set
        End Property

        Public Property Country() As Object
            Get
                Return _Country
            End Get
            Set(ByVal value As Object)
                _Country = value
            End Set
        End Property

        Public Property HomePhone() As Object
            Get
                Return _HomePhone
            End Get
            Set(ByVal value As Object)
                _HomePhone = value
            End Set
        End Property

        Public Property Extension() As Object
            Get
                Return _Extension
            End Get
            Set(ByVal value As Object)
                _Extension = value
            End Set
        End Property

        Public Property Photo() As Object
            Get
                Return _Photo
            End Get
            Set(ByVal value As Object)
                _Photo = value
            End Set
        End Property

        Public Property Notes() As Object
            Get
                Return _Notes
            End Get
            Set(ByVal value As Object)
                _Notes = value
            End Set
        End Property

        Public Property ReportsTo() As Object
            Get
                Return _ReportsTo
            End Get
            Set(ByVal value As Object)
                _ReportsTo = value
            End Set
        End Property

        Public Property PhotoPath() As Object
            Get
                Return _PhotoPath
            End Get
            Set(ByVal value As Object)
                _PhotoPath = value
            End Set
        End Property

        Public Property UserID() As Object
            Get
                Return _UserID
            End Get
            Set(ByVal value As Object)
                _UserID = value
            End Set
        End Property

        Public Property EmpGroupID() As Object
            Get
                Return _EmpGroupID
            End Get
            Set(ByVal value As Object)
                _EmpGroupID = value
            End Set
        End Property

        Public Property Email() As Object
            Get
                Return _Email
            End Get
            Set(ByVal value As Object)
                _Email = value
            End Set
        End Property

        Public Property Province() As Object
            Get
                Return _Province
            End Get
            Set(ByVal value As Object)
                _Province = value
            End Set
        End Property

        Public Property Amphur() As Object
            Get
                Return _Amphur
            End Get
            Set(ByVal value As Object)
                _Amphur = value
            End Set
        End Property

        Public Property District() As Object
            Get
                Return _District
            End Get
            Set(ByVal value As Object)
                _District = value
            End Set
        End Property

        Public Property LOCATION_CODE() As Object
            Get
                Return _LOCATION_CODE
            End Get
            Set(ByVal value As Object)
                _LOCATION_CODE = value
            End Set
        End Property

        Public Property TempUserName() As Object
            Get
                Return _TempUserName
            End Get
            Set(ByVal value As Object)
                _TempUserName = value
            End Set
        End Property

        Public Property TempPassword() As Object
            Get
                Return _TempPassword
            End Get
            Set(ByVal value As Object)
                _TempPassword = value
            End Set
        End Property

        Public Property TempPasswordQuestion() As Object
            Get
                Return _TempPasswordQuestion
            End Get
            Set(ByVal value As Object)
                _TempPasswordQuestion = value
            End Set
        End Property

        Public Property TempPasswordAnswer() As Object
            Get
                Return _TempPasswordAnswer
            End Get
            Set(ByVal value As Object)
                _TempPasswordAnswer = value
            End Set
        End Property

        Public Property IsActivate() As Object
            Get
                Return _IsActivate
            End Get
            Set(ByVal value As Object)
                _IsActivate = value
            End Set
        End Property

        Public Property ActivateCode() As Object
            Get
                Return _ActivateCode
            End Get
            Set(ByVal value As Object)
                _ActivateCode = value
            End Set
        End Property

        Public Property ActivationDate() As Object
            Get
                Return _ActivationDate
            End Get
            Set(ByVal value As Object)
                _ActivationDate = value
            End Set
        End Property



    End Class
    <Serializable()> _
    Public Class SingleSignOn_Users
        Private _UserID As Object
        Private _UserName As Object
        Private _Name As Object
        Private _UserType As Object
        Private _Level As Object
        Public Property UserID() As Object
            Get
                Return _UserID
            End Get
            Set(ByVal value As Object)
                _UserID = value
            End Set
        End Property

        Public Property UserName() As Object
            Get
                Return _UserName
            End Get
            Set(ByVal value As Object)
                _UserName = value
            End Set
        End Property

        Public Property Name() As Object
            Get
                Return _Name
            End Get
            Set(ByVal value As Object)
                _Name = value
            End Set
        End Property

        Public Property UserType() As Object
            Get
                Return _UserType
            End Get
            Set(ByVal value As Object)
                _UserType = value
            End Set
        End Property

        Public Property Level() As Object
            Get
                Return _Level
            End Get
            Set(ByVal value As Object)
                _Level = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class JQueryFeature
        Private _id As String

        Public Property Id() As String
            Get
                Return _id
            End Get
            Set(value As String)
                _id = value
            End Set
        End Property
        Private _name As String

        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        Private _level As Integer

        Public Property Level() As Integer
            Get
                Return _level
            End Get
            Set(value As Integer)
                _level = value
            End Set
        End Property

        Private _enableSelect As Boolean

        Public Property EnableSelect() As Boolean
            Get
                Return _enableSelect
            End Get
            Set(value As Boolean)
                _enableSelect = value
            End Set
        End Property

        Public Sub New(id As String, name As String, level As Integer, enableSelect As Boolean)
            _id = id
            _name = name
            _level = level
            _enableSelect = enableSelect
        End Sub

        Public Overrides Function ToString() As String
            Return [String].Format("Name:{0}+Id:{1}", Name, Id)
        End Function
    End Class
End Namespace


