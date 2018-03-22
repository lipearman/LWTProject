Imports Microsoft.VisualBasic

Namespace MyContext
    <Serializable()> _
    Public Class Process_Result
        Private _Code As String
        Public Property Code() As String
            Get
                Return _Code
            End Get
            Set(ByVal value As String)
                _Code = value
            End Set
        End Property

        Private _Name As String
        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property



        Private _Status As String
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property
    End Class


    '<Serializable()> _
    'Public Class TreeMenuRoles
    '    Private _Id As Integer
    '    Public Property Id() As Integer
    '        Get
    '            Return _Id
    '        End Get
    '        Set(ByVal value As Integer)
    '            _Id = value
    '        End Set
    '    End Property
    '    Private _MyText As String
    '    Public Property MyText() As String
    '        Get
    '            Return _MyText
    '        End Get
    '        Set(ByVal value As String)
    '            _MyText = value
    '        End Set
    '    End Property
    '    Private _URL As String
    '    Public Property URL() As String
    '        Get
    '            Return _URL
    '        End Get
    '        Set(ByVal value As String)
    '            _URL = value
    '        End Set
    '    End Property
    '    Private _IsLeaf As Integer
    '    Public Property IsLeaf() As Integer
    '        Get
    '            Return _IsLeaf
    '        End Get
    '        Set(ByVal value As Integer)
    '            _IsLeaf = value
    '        End Set
    '    End Property
    '    Private _ParentId As Integer
    '    Public Property ParentId() As Integer
    '        Get
    '            Return _ParentId
    '        End Get
    '        Set(ByVal value As Integer)
    '            _ParentId = value
    '        End Set
    '    End Property
    '    Private _TabOrder As Integer
    '    Public Property TabOrder() As Integer
    '        Get
    '            Return _TabOrder
    '        End Get
    '        Set(ByVal value As Integer)
    '            _TabOrder = value
    '        End Set
    '    End Property
    '    Private _AuthorizedRoles As String
    '    Public Property AuthorizedRoles() As String
    '        Get
    '            Return _AuthorizedRoles
    '        End Get
    '        Set(ByVal value As String)
    '            _AuthorizedRoles = value
    '        End Set
    '    End Property


    'End Class
    <Serializable()> _
    Public Class SignInDomain
        Private _UserProfile As List(Of Portal.Components.UserTab)
        Public Property UserProfile() As List(Of Portal.Components.UserTab)
            Get
                Return _UserProfile
            End Get
            Set(ByVal value As List(Of Portal.Components.UserTab))
                _UserProfile = value
            End Set
        End Property
        Private _Result As SignInDomain_Result
        Public Property Result() As SignInDomain_Result
            Get
                Return _Result
            End Get
            Set(ByVal value As SignInDomain_Result)
                _Result = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class SignInDomain_Result
        Private _Success As Boolean
        Public Property Success() As Boolean
            Get
                Return _Success
            End Get
            Set(ByVal value As Boolean)
                _Success = value
            End Set
        End Property
        Private _Message As String
        Public Property Message() As String
            Get
                Return _Message
            End Get
            Set(ByVal value As String)
                _Message = value
            End Set
        End Property
    End Class


    <Serializable()> _
    Public Class TreeMenuRoles
        Private _Id As Integer
        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property
        Private _MyText As String
        Public Property MyText() As String
            Get
                Return _MyText
            End Get
            Set(ByVal value As String)
                _MyText = value
            End Set
        End Property
        Private _URL As String
        Public Property URL() As String
            Get
                Return _URL
            End Get
            Set(ByVal value As String)
                _URL = value
            End Set
        End Property
        Private _IsLeaf As Integer
        Public Property IsLeaf() As Integer
            Get
                Return _IsLeaf
            End Get
            Set(ByVal value As Integer)
                _IsLeaf = value
            End Set
        End Property
        Private _ParentId As Integer
        Public Property ParentId() As Integer
            Get
                Return _ParentId
            End Get
            Set(ByVal value As Integer)
                _ParentId = value
            End Set
        End Property
        Private _TabOrder As Integer
        Public Property TabOrder() As Integer
            Get
                Return _TabOrder
            End Get
            Set(ByVal value As Integer)
                _TabOrder = value
            End Set
        End Property
        Private _AuthorizedRoles As String
        Public Property AuthorizedRoles() As String
            Get
                Return _AuthorizedRoles
            End Get
            Set(ByVal value As String)
                _AuthorizedRoles = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class PortalCfg_Globals
        Private _PortalId As Object
        Public Property PortalId() As Object
            Get
                Return _PortalId
            End Get
            Set(ByVal value As Object)
                _PortalId = value
            End Set
        End Property
        Private _PortalName As Object
        Public Property PortalName() As Object
            Get
                Return _PortalName
            End Get
            Set(ByVal value As Object)
                _PortalName = value
            End Set
        End Property
        Private _PortalCode As Object
        Public Property PortalCode() As Object
            Get
                Return _PortalCode
            End Get
            Set(ByVal value As Object)
                _PortalCode = value
            End Set
        End Property
        Private _PortalPage As Object
        Public Property PortalPage() As Object
            Get
                Return _PortalPage
            End Get
            Set(ByVal value As Object)
                _PortalPage = value
            End Set
        End Property
        Private _AlwaysShowEditButton As Object
        Public Property AlwaysShowEditButton() As Object
            Get
                Return _AlwaysShowEditButton
            End Get
            Set(ByVal value As Object)
                _AlwaysShowEditButton = value
            End Set
        End Property
        Private _UnderConstruction As Object
        Public Property UnderConstruction() As Object
            Get
                Return _UnderConstruction
            End Get
            Set(ByVal value As Object)
                _UnderConstruction = value
            End Set
        End Property

        Private _CreateDate As Object
        Public Property CreateDate() As Object
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As Object)
                _CreateDate = value
            End Set
        End Property

        Private _ModifyDate As Object
        Public Property ModifyDate() As Object
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Object)
                _ModifyDate = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProjectRoles
        Private _RoleID As Object
        Public Property RoleID() As Object
            Get
                Return _RoleID
            End Get
            Set(ByVal value As Object)
                _RoleID = value
            End Set
        End Property

        Private _RoleName As Object
        Public Property RoleName() As Object
            Get
                Return _RoleName
            End Get
            Set(ByVal value As Object)
                _RoleName = value
            End Set
        End Property

        Private _RoleDescription As Object
        Public Property RoleDescription() As Object
            Get
                Return _RoleDescription
            End Get
            Set(ByVal value As Object)
                _RoleDescription = value
            End Set
        End Property


        Private _RoleCode As Object
        Public Property RoleCode() As Object
            Get
                Return _RoleCode
            End Get
            Set(ByVal value As Object)
                _RoleCode = value
            End Set
        End Property


        Private _PortalID As Object
        Public Property PortalID() As Object
            Get
                Return _PortalID
            End Get
            Set(ByVal value As Object)
                _PortalID = value
            End Set
        End Property

        Private _PortalName As Object
        Public Property PortalName() As Object
            Get
                Return _PortalName
            End Get
            Set(ByVal value As Object)
                _PortalName = value
            End Set
        End Property


        Private _CreateDate As Object
        Public Property CreateDate() As Object
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As Object)
                _CreateDate = value
            End Set
        End Property

        Private _ModifyDate As Object
        Public Property ModifyDate() As Object
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Object)
                _ModifyDate = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ProjectModuleDef
        Private _ModuleDefId As Object
        Public Property ModuleDefId() As Object
            Get
                Return _ModuleDefId
            End Get
            Set(ByVal value As Object)
                _ModuleDefId = value
            End Set
        End Property

        Private _ModuleDefName As Object
        Public Property ModuleDefName() As Object
            Get
                Return _ModuleDefName
            End Get
            Set(ByVal value As Object)
                _ModuleDefName = value
            End Set
        End Property

        Private _ModuleDefSourceFile As Object
        Public Property ModuleDefSourceFile() As Object
            Get
                Return _ModuleDefSourceFile
            End Get
            Set(ByVal value As Object)
                _ModuleDefSourceFile = value
            End Set
        End Property

        Private _ModuleDefMobileSourceFile As Object
        Public Property ModuleDefMobileSourceFile() As Object
            Get
                Return _ModuleDefMobileSourceFile
            End Get
            Set(ByVal value As Object)
                _ModuleDefMobileSourceFile = value
            End Set
        End Property

        Private _ModuleDefCode As Object
        Public Property ModuleDefCode() As Object
            Get
                Return _ModuleDefCode
            End Get
            Set(ByVal value As Object)
                _ModuleDefCode = value
            End Set
        End Property

        Private _ModuleDefDesc As Object
        Public Property ModuleDefDesc() As Object
            Get
                Return _ModuleDefDesc
            End Get
            Set(ByVal value As Object)
                _ModuleDefDesc = value
            End Set
        End Property

        Private _PortalName As Object
        Public Property PortalName() As Object
            Get
                Return _PortalName
            End Get
            Set(ByVal value As Object)
                _PortalName = value
            End Set
        End Property

        Private _CreateDate As Object
        Public Property CreateDate() As Object
            Get
                Return _CreateDate
            End Get
            Set(ByVal value As Object)
                _CreateDate = value
            End Set
        End Property

        Private _ModifyDate As Object
        Public Property ModifyDate() As Object
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Object)
                _ModifyDate = value
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

    <Serializable()> _
    Public Class Portal_User
        Private _UserID As Object
        Private _UserName As Object
        Private _Password As Object
        Private _Email As Object
        Private _Comment As Object
        Private _PasswordQuestion As Object
        Private _PasswordAnswer As Object
        Private _IsApproved As Object
        Private _LastActivityDate As Object
        Private _LastLoginDate As Object
        Private _LastPasswordChangedDate As Object
        Private _CreationDate As Object
        Private _IsLocked As Object
        Private _LastLockOutDate As Object
        Private _FailedPasswordAttemptCount As Object
        Private _FailedPasswordAttemptWindowStart As Object
        Private _FailedPasswordAnswerAttemptCount As Object
        Private _FailedPasswordAnswerAttemptWindowStart As Object
        Private _ExpiredDate As Object


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

        Public Property Password() As Object
            Get
                Return _Password
            End Get
            Set(ByVal value As Object)
                _Password = value
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

        Public Property Comment() As Object
            Get
                Return _Comment
            End Get
            Set(ByVal value As Object)
                _Comment = value
            End Set
        End Property

        Public Property PasswordQuestion() As Object
            Get
                Return _PasswordQuestion
            End Get
            Set(ByVal value As Object)
                _PasswordQuestion = value
            End Set
        End Property

        Public Property PasswordAnswer() As Object
            Get
                Return _PasswordAnswer
            End Get
            Set(ByVal value As Object)
                _PasswordAnswer = value
            End Set
        End Property

        Public Property IsApproved() As Object
            Get
                Return _IsApproved
            End Get
            Set(ByVal value As Object)
                _IsApproved = value
            End Set
        End Property

        Public Property LastActivityDate() As Object
            Get
                Return _LastActivityDate
            End Get
            Set(ByVal value As Object)
                _LastActivityDate = value
            End Set
        End Property

        Public Property LastLoginDate() As Object
            Get
                Return _LastLoginDate
            End Get
            Set(ByVal value As Object)
                _LastLoginDate = value
            End Set
        End Property

        Public Property LastPasswordChangedDate() As Object
            Get
                Return _LastPasswordChangedDate
            End Get
            Set(ByVal value As Object)
                _LastPasswordChangedDate = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property

        Public Property IsLocked() As Object
            Get
                Return _IsLocked
            End Get
            Set(ByVal value As Object)
                _IsLocked = value
            End Set
        End Property

        Public Property LastLockOutDate() As Object
            Get
                Return _LastLockOutDate
            End Get
            Set(ByVal value As Object)
                _LastLockOutDate = value
            End Set
        End Property

        Public Property FailedPasswordAttemptCount() As Object
            Get
                Return _FailedPasswordAttemptCount
            End Get
            Set(ByVal value As Object)
                _FailedPasswordAttemptCount = value
            End Set
        End Property

        Public Property FailedPasswordAttemptWindowStart() As Object
            Get
                Return _FailedPasswordAttemptWindowStart
            End Get
            Set(ByVal value As Object)
                _FailedPasswordAttemptWindowStart = value
            End Set
        End Property

        Public Property FailedPasswordAnswerAttemptCount() As Object
            Get
                Return _FailedPasswordAnswerAttemptCount
            End Get
            Set(ByVal value As Object)
                _FailedPasswordAnswerAttemptCount = value
            End Set
        End Property

        Public Property FailedPasswordAnswerAttemptWindowStart() As Object
            Get
                Return _FailedPasswordAnswerAttemptWindowStart
            End Get
            Set(ByVal value As Object)
                _FailedPasswordAnswerAttemptWindowStart = value
            End Set
        End Property

        Public Property ExpiredDate() As Object
            Get
                Return _ExpiredDate
            End Get
            Set(ByVal value As Object)
                _ExpiredDate = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class Employee
        Private _objectGUID As Object
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

        Public Property objectGUID() As Object
            Get
                Return _objectGUID
            End Get
            Set(ByVal value As Object)
                _objectGUID = value
            End Set
        End Property
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
    Public Class MailNotifications
        Private _ID As Object
        Private _Code As Object
        Private _Name As Object
        Private _MailFrom As Object
        Private _MailTo As Object
        Private _MailCC As Object
        Private _MailBcc As Object
        Private _MailSubject As Object
        Private _MailBody As Object
        Private _IsActive As Object
        Private _CreationDate As Object
        Private _ModifiedDate As Object

        Public Property ID() As Object
            Get
                Return _ID
            End Get
            Set(ByVal value As Object)
                _ID = value
            End Set
        End Property

        Public Property Code() As Object
            Get
                Return _Code
            End Get
            Set(ByVal value As Object)
                _Code = value
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

        Public Property MailFrom() As Object
            Get
                Return _MailFrom
            End Get
            Set(ByVal value As Object)
                _MailFrom = value
            End Set
        End Property

        Public Property MailTo() As Object
            Get
                Return _MailTo
            End Get
            Set(ByVal value As Object)
                _MailTo = value
            End Set
        End Property

        Public Property MailCC() As Object
            Get
                Return _MailCC
            End Get
            Set(ByVal value As Object)
                _MailCC = value
            End Set
        End Property

        Public Property MailBcc() As Object
            Get
                Return _MailBcc
            End Get
            Set(ByVal value As Object)
                _MailBcc = value
            End Set
        End Property

        Public Property MailSubject() As Object
            Get
                Return _MailSubject
            End Get
            Set(ByVal value As Object)
                _MailSubject = value
            End Set
        End Property

        Public Property MailBody() As Object
            Get
                Return _MailBody
            End Get
            Set(ByVal value As Object)
                _MailBody = value
            End Set
        End Property

        Public Property IsActive() As Object
            Get
                Return _IsActive
            End Get
            Set(ByVal value As Object)
                _IsActive = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property

        Public Property ModifiedDate() As Object
            Get
                Return _ModifiedDate
            End Get
            Set(ByVal value As Object)
                _ModifiedDate = value
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
    Public Class tbProjects
        Private _ProjectID As Object
        Private _ProjectCode As Object
        Private _ProjectName As Object
        Private _CreationDate As Object
        Private _ExpiryDate As Object
        Private _ModifyDate As Object
        Private _CreationBy As Object
        Private _ModifyBy As Object
        Private _IsActive As Object
        Private _StartDate As Object

        Public Property ProjectID() As Object
            Get
                Return _ProjectID
            End Get
            Set(ByVal value As Object)
                _ProjectID = value
            End Set
        End Property

        Public Property ProjectCode() As Object
            Get
                Return _ProjectCode
            End Get
            Set(ByVal value As Object)
                _ProjectCode = value
            End Set
        End Property

        Public Property ProjectName() As Object
            Get
                Return _ProjectName
            End Get
            Set(ByVal value As Object)
                _ProjectName = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property
        Public Property StartDate() As Object
            Get
                Return _StartDate
            End Get
            Set(ByVal value As Object)
                _StartDate = value
            End Set
        End Property
        Public Property ExpiryDate() As Object
            Get
                Return _ExpiryDate
            End Get
            Set(ByVal value As Object)
                _ExpiryDate = value
            End Set
        End Property

        Public Property ModifyDate() As Object
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Object)
                _ModifyDate = value
            End Set
        End Property

        Public Property CreationBy() As Object
            Get
                Return _CreationBy
            End Get
            Set(ByVal value As Object)
                _CreationBy = value
            End Set
        End Property

        Public Property ModifyBy() As Object
            Get
                Return _ModifyBy
            End Get
            Set(ByVal value As Object)
                _ModifyBy = value
            End Set
        End Property

        Public Property IsActive() As Object
            Get
                Return _IsActive
            End Get
            Set(ByVal value As Object)
                _IsActive = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class V_MASTER_COR
        Private _Risk As Object
        Private _Description As Object
        Private _RiskGroupI As Object
        Private _RiskGroupII As Object
        Private _RiskShortDesc As Object
        Private _RiskGovernment As Object
        Private _Department As Object
        Private _RISK_TYPE As Object
        Public Property Risk() As Object
            Get
                Return _Risk
            End Get
            Set(ByVal value As Object)
                _Risk = value
            End Set
        End Property

        Public Property Description() As Object
            Get
                Return _Description
            End Get
            Set(ByVal value As Object)
                _Description = value
            End Set
        End Property

        Public Property RiskGroupI() As Object
            Get
                Return _RiskGroupI
            End Get
            Set(ByVal value As Object)
                _RiskGroupI = value
            End Set
        End Property

        Public Property RiskGroupII() As Object
            Get
                Return _RiskGroupII
            End Get
            Set(ByVal value As Object)
                _RiskGroupII = value
            End Set
        End Property

        Public Property RiskShortDesc() As Object
            Get
                Return _RiskShortDesc
            End Get
            Set(ByVal value As Object)
                _RiskShortDesc = value
            End Set
        End Property

        Public Property RiskGovernment() As Object
            Get
                Return _RiskGovernment
            End Get
            Set(ByVal value As Object)
                _RiskGovernment = value
            End Set
        End Property

        Public Property Department() As Object
            Get
                Return _Department
            End Get
            Set(ByVal value As Object)
                _Department = value
            End Set
        End Property

        Public Property RISK_TYPE() As Object
            Get
                Return _RISK_TYPE
            End Get
            Set(ByVal value As Object)
                _RISK_TYPE = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class V_MASTER_UW
        Private _Underwriter As Object
        Private _CrossReference As Object
        Private _Name As Object
        Private _Address1 As Object
        Private _Address2 As Object
        Private _Address3 As Object
        Private _PostCode As Object
        Private _City As Object
        Private _PhoneBusiness As Object
        Private _PhoneHome As Object
        Private _Facsimile As Object
        Private _AccountContact As Object
        Private _Addressee As Object
        Private _Salutation As Object
        Private _DaysCredit As Object
        Private _TrueUnderwriter As Object
        Private _EntryBy As Object
        Private _EntryDate As Object
        Private _FinanceContact As Object
        Private _GeneralClaimContact As Object
        Private _Type As Object
        Private _InsuranceLine As Object
        Private _VATPayType As Object
        Private _PhoneFinance As Object
        Private _PhoneClaims As Object
        Private _FaxFinance As Object
        Private _FaxClaims As Object
        Public Property Underwriter() As Object
            Get
                Return _Underwriter
            End Get
            Set(ByVal value As Object)
                _Underwriter = value
            End Set
        End Property

        Public Property CrossReference() As Object
            Get
                Return _CrossReference
            End Get
            Set(ByVal value As Object)
                _CrossReference = value
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

        Public Property Address1() As Object
            Get
                Return _Address1
            End Get
            Set(ByVal value As Object)
                _Address1 = value
            End Set
        End Property

        Public Property Address2() As Object
            Get
                Return _Address2
            End Get
            Set(ByVal value As Object)
                _Address2 = value
            End Set
        End Property

        Public Property Address3() As Object
            Get
                Return _Address3
            End Get
            Set(ByVal value As Object)
                _Address3 = value
            End Set
        End Property

        Public Property PostCode() As Object
            Get
                Return _PostCode
            End Get
            Set(ByVal value As Object)
                _PostCode = value
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

        Public Property PhoneBusiness() As Object
            Get
                Return _PhoneBusiness
            End Get
            Set(ByVal value As Object)
                _PhoneBusiness = value
            End Set
        End Property

        Public Property PhoneHome() As Object
            Get
                Return _PhoneHome
            End Get
            Set(ByVal value As Object)
                _PhoneHome = value
            End Set
        End Property

        Public Property Facsimile() As Object
            Get
                Return _Facsimile
            End Get
            Set(ByVal value As Object)
                _Facsimile = value
            End Set
        End Property

        Public Property AccountContact() As Object
            Get
                Return _AccountContact
            End Get
            Set(ByVal value As Object)
                _AccountContact = value
            End Set
        End Property

        Public Property Addressee() As Object
            Get
                Return _Addressee
            End Get
            Set(ByVal value As Object)
                _Addressee = value
            End Set
        End Property

        Public Property Salutation() As Object
            Get
                Return _Salutation
            End Get
            Set(ByVal value As Object)
                _Salutation = value
            End Set
        End Property

        Public Property DaysCredit() As Object
            Get
                Return _DaysCredit
            End Get
            Set(ByVal value As Object)
                _DaysCredit = value
            End Set
        End Property

        Public Property TrueUnderwriter() As Object
            Get
                Return _TrueUnderwriter
            End Get
            Set(ByVal value As Object)
                _TrueUnderwriter = value
            End Set
        End Property

        Public Property EntryBy() As Object
            Get
                Return _EntryBy
            End Get
            Set(ByVal value As Object)
                _EntryBy = value
            End Set
        End Property

        Public Property EntryDate() As Object
            Get
                Return _EntryDate
            End Get
            Set(ByVal value As Object)
                _EntryDate = value
            End Set
        End Property

        Public Property FinanceContact() As Object
            Get
                Return _FinanceContact
            End Get
            Set(ByVal value As Object)
                _FinanceContact = value
            End Set
        End Property

        Public Property GeneralClaimContact() As Object
            Get
                Return _GeneralClaimContact
            End Get
            Set(ByVal value As Object)
                _GeneralClaimContact = value
            End Set
        End Property

        Public Property Type() As Object
            Get
                Return _Type
            End Get
            Set(ByVal value As Object)
                _Type = value
            End Set
        End Property

        Public Property InsuranceLine() As Object
            Get
                Return _InsuranceLine
            End Get
            Set(ByVal value As Object)
                _InsuranceLine = value
            End Set
        End Property

        Public Property VATPayType() As Object
            Get
                Return _VATPayType
            End Get
            Set(ByVal value As Object)
                _VATPayType = value
            End Set
        End Property

        Public Property PhoneFinance() As Object
            Get
                Return _PhoneFinance
            End Get
            Set(ByVal value As Object)
                _PhoneFinance = value
            End Set
        End Property

        Public Property PhoneClaims() As Object
            Get
                Return _PhoneClaims
            End Get
            Set(ByVal value As Object)
                _PhoneClaims = value
            End Set
        End Property

        Public Property FaxFinance() As Object
            Get
                Return _FaxFinance
            End Get
            Set(ByVal value As Object)
                _FaxFinance = value
            End Set
        End Property

        Public Property FaxClaims() As Object
            Get
                Return _FaxClaims
            End Get
            Set(ByVal value As Object)
                _FaxClaims = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class V_MASTER_AGENT
        Private _Agent As Object
        Private _Name As Object
        Private _Address1 As Object
        Private _Address2 As Object
        Private _Address3 As Object
        Private _PostCode As Object
        Private _City As Object
        Private _PhoneBusiness As Object
        Private _PhoneHome As Object
        Private _ContactPerson As Object
        Private _Addressee As Object
        Private _Salutation As Object
        Private _Occupation As Object
        Private _EntryBy As Object
        Private _EntryDate As Object
        Private _IsSubAgent As Object
        Private _InternetAddress As Object

        Public Property Agent() As Object
            Get
                Return _Agent
            End Get
            Set(ByVal value As Object)
                _Agent = value
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

        Public Property Address1() As Object
            Get
                Return _Address1
            End Get
            Set(ByVal value As Object)
                _Address1 = value
            End Set
        End Property

        Public Property Address2() As Object
            Get
                Return _Address2
            End Get
            Set(ByVal value As Object)
                _Address2 = value
            End Set
        End Property

        Public Property Address3() As Object
            Get
                Return _Address3
            End Get
            Set(ByVal value As Object)
                _Address3 = value
            End Set
        End Property

        Public Property PostCode() As Object
            Get
                Return _PostCode
            End Get
            Set(ByVal value As Object)
                _PostCode = value
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

        Public Property PhoneBusiness() As Object
            Get
                Return _PhoneBusiness
            End Get
            Set(ByVal value As Object)
                _PhoneBusiness = value
            End Set
        End Property

        Public Property PhoneHome() As Object
            Get
                Return _PhoneHome
            End Get
            Set(ByVal value As Object)
                _PhoneHome = value
            End Set
        End Property

        Public Property ContactPerson() As Object
            Get
                Return _ContactPerson
            End Get
            Set(ByVal value As Object)
                _ContactPerson = value
            End Set
        End Property

        Public Property Addressee() As Object
            Get
                Return _Addressee
            End Get
            Set(ByVal value As Object)
                _Addressee = value
            End Set
        End Property

        Public Property Salutation() As Object
            Get
                Return _Salutation
            End Get
            Set(ByVal value As Object)
                _Salutation = value
            End Set
        End Property

        Public Property Occupation() As Object
            Get
                Return _Occupation
            End Get
            Set(ByVal value As Object)
                _Occupation = value
            End Set
        End Property

        Public Property EntryBy() As Object
            Get
                Return _EntryBy
            End Get
            Set(ByVal value As Object)
                _EntryBy = value
            End Set
        End Property

        Public Property EntryDate() As Object
            Get
                Return _EntryDate
            End Get
            Set(ByVal value As Object)
                _EntryDate = value
            End Set
        End Property

        Public Property IsSubAgent() As Object
            Get
                Return _IsSubAgent
            End Get
            Set(ByVal value As Object)
                _IsSubAgent = value
            End Set
        End Property

        Public Property InternetAddress() As Object
            Get
                Return _InternetAddress
            End Get
            Set(ByVal value As Object)
                _InternetAddress = value
            End Set
        End Property



    End Class

    <Serializable()> _
    Public Class V_COR_CommIn
        Private _Underwriter As Object
        Private _Name As Object
        Private _CrossReference As Object
        Private _Risk As Object
        Private _Description As Object
        Private _RiskGovernment As Object
        Private _RISK_TYPE As Object
        Private _Brokerage As Object
        Private _ORCommissionPercent As Object
        Private _AdminFeeIn1 As Object
        Private _AdminFeeIn2 As Object
        Private _AgentCommission As Object
        Private _PremiumWarantydays As Object
        Private _NewRenewaldays As Object
        Private _NewNewBusdays As Object
        Private _RenRenewaldays As Object
        Private _RenNewBusdays As Object
        Private _AutoCalculation As Object
        Private _OffsetORFlag As Object
        Private _OffsetAdm1Flag As Object
        Private _OffsetAdm2Flag As Object
        Private _ORCalFrom As Object
        Private _OROutRate As Object
        Public Property Underwriter() As Object
            Get
                Return _Underwriter
            End Get
            Set(ByVal value As Object)
                _Underwriter = value
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

        Public Property CrossReference() As Object
            Get
                Return _CrossReference
            End Get
            Set(ByVal value As Object)
                _CrossReference = value
            End Set
        End Property

        Public Property Risk() As Object
            Get
                Return _Risk
            End Get
            Set(ByVal value As Object)
                _Risk = value
            End Set
        End Property

        Public Property Description() As Object
            Get
                Return _Description
            End Get
            Set(ByVal value As Object)
                _Description = value
            End Set
        End Property

        Public Property RiskGovernment() As Object
            Get
                Return _RiskGovernment
            End Get
            Set(ByVal value As Object)
                _RiskGovernment = value
            End Set
        End Property

        Public Property RISK_TYPE() As Object
            Get
                Return _RISK_TYPE
            End Get
            Set(ByVal value As Object)
                _RISK_TYPE = value
            End Set
        End Property

        Public Property Brokerage() As Object
            Get
                Return _Brokerage
            End Get
            Set(ByVal value As Object)
                _Brokerage = value
            End Set
        End Property

        Public Property ORCommissionPercent() As Object
            Get
                Return _ORCommissionPercent
            End Get
            Set(ByVal value As Object)
                _ORCommissionPercent = value
            End Set
        End Property

        Public Property AdminFeeIn1() As Object
            Get
                Return _AdminFeeIn1
            End Get
            Set(ByVal value As Object)
                _AdminFeeIn1 = value
            End Set
        End Property

        Public Property AdminFeeIn2() As Object
            Get
                Return _AdminFeeIn2
            End Get
            Set(ByVal value As Object)
                _AdminFeeIn2 = value
            End Set
        End Property

        Public Property AgentCommission() As Object
            Get
                Return _AgentCommission
            End Get
            Set(ByVal value As Object)
                _AgentCommission = value
            End Set
        End Property

        Public Property PremiumWarantydays() As Object
            Get
                Return _PremiumWarantydays
            End Get
            Set(ByVal value As Object)
                _PremiumWarantydays = value
            End Set
        End Property

        Public Property NewRenewaldays() As Object
            Get
                Return _NewRenewaldays
            End Get
            Set(ByVal value As Object)
                _NewRenewaldays = value
            End Set
        End Property

        Public Property NewNewBusdays() As Object
            Get
                Return _NewNewBusdays
            End Get
            Set(ByVal value As Object)
                _NewNewBusdays = value
            End Set
        End Property

        Public Property RenRenewaldays() As Object
            Get
                Return _RenRenewaldays
            End Get
            Set(ByVal value As Object)
                _RenRenewaldays = value
            End Set
        End Property

        Public Property RenNewBusdays() As Object
            Get
                Return _RenNewBusdays
            End Get
            Set(ByVal value As Object)
                _RenNewBusdays = value
            End Set
        End Property

        Public Property AutoCalculation() As Object
            Get
                Return _AutoCalculation
            End Get
            Set(ByVal value As Object)
                _AutoCalculation = value
            End Set
        End Property

        Public Property OffsetORFlag() As Object
            Get
                Return _OffsetORFlag
            End Get
            Set(ByVal value As Object)
                _OffsetORFlag = value
            End Set
        End Property

        Public Property OffsetAdm1Flag() As Object
            Get
                Return _OffsetAdm1Flag
            End Get
            Set(ByVal value As Object)
                _OffsetAdm1Flag = value
            End Set
        End Property

        Public Property OffsetAdm2Flag() As Object
            Get
                Return _OffsetAdm2Flag
            End Get
            Set(ByVal value As Object)
                _OffsetAdm2Flag = value
            End Set
        End Property

        Public Property ORCalFrom() As Object
            Get
                Return _ORCalFrom
            End Get
            Set(ByVal value As Object)
                _ORCalFrom = value
            End Set
        End Property

        Public Property OROutRate() As Object
            Get
                Return _OROutRate
            End Get
            Set(ByVal value As Object)
                _OROutRate = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class V_COR_CommOut
        Private _Agent As Object
        Private _Name As Object
        Private _Risk As Object
        Private _RiskGovernment As Object
        Private _RISK_TYPE As Object
        Private _CommissionOut As Object
        Private _OROut As Object
        Private _AdminOut1 As Object
        Private _AdminOut2 As Object
        Private _OROutCalFrom As Object


        Public Property Agent() As Object
            Get
                Return _Agent
            End Get
            Set(ByVal value As Object)
                _Agent = value
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

        Public Property Risk() As Object
            Get
                Return _Risk
            End Get
            Set(ByVal value As Object)
                _Risk = value
            End Set
        End Property
        Public Property RiskGovernment() As Object
            Get
                Return _RiskGovernment
            End Get
            Set(ByVal value As Object)
                _RiskGovernment = value
            End Set
        End Property
        Public Property RISK_TYPE() As Object
            Get
                Return _RISK_TYPE
            End Get
            Set(ByVal value As Object)
                _RISK_TYPE = value
            End Set
        End Property

        Public Property CommissionOut() As Object
            Get
                Return _CommissionOut
            End Get
            Set(ByVal value As Object)
                _CommissionOut = value
            End Set
        End Property

        Public Property OROut() As Object
            Get
                Return _OROut
            End Get
            Set(ByVal value As Object)
                _OROut = value
            End Set
        End Property

        Public Property AdminOut1() As Object
            Get
                Return _AdminOut1
            End Get
            Set(ByVal value As Object)
                _AdminOut1 = value
            End Set
        End Property

        Public Property AdminOut2() As Object
            Get
                Return _AdminOut2
            End Get
            Set(ByVal value As Object)
                _AdminOut2 = value
            End Set
        End Property

        Public Property OROutCalFrom() As Object
            Get
                Return _OROutCalFrom
            End Get
            Set(ByVal value As Object)
                _OROutCalFrom = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class tblScheme
        Private _SchemeID As Object
        Private _SchemeCode As Object
        Private _SchemeName As Object
        Private _CreationDate As Object
        Private _ExpiryDate As Object
        Private _ModifyDate As Object
        Private _CreationBy As Object
        Private _ModifyBy As Object
        Private _IsActive As Object
        Private _ProjectCode As Object
        Private _ParentSchemeID As Object
        Private _StartDate As Object
        Private _RiskGovernment As Object
        Private _RiskType As Object
        Private _PolType As Object

        Public Property SchemeID() As Object
            Get
                Return _SchemeID
            End Get
            Set(ByVal value As Object)
                _SchemeID = value
            End Set
        End Property

        Public Property SchemeCode() As Object
            Get
                Return _SchemeCode
            End Get
            Set(ByVal value As Object)
                _SchemeCode = value
            End Set
        End Property

        Public Property SchemeName() As Object
            Get
                Return _SchemeName
            End Get
            Set(ByVal value As Object)
                _SchemeName = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property

        Public Property ExpiryDate() As Object
            Get
                Return _ExpiryDate
            End Get
            Set(ByVal value As Object)
                _ExpiryDate = value
            End Set
        End Property

        Public Property ModifyDate() As Object
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Object)
                _ModifyDate = value
            End Set
        End Property

        Public Property CreationBy() As Object
            Get
                Return _CreationBy
            End Get
            Set(ByVal value As Object)
                _CreationBy = value
            End Set
        End Property

        Public Property ModifyBy() As Object
            Get
                Return _ModifyBy
            End Get
            Set(ByVal value As Object)
                _ModifyBy = value
            End Set
        End Property

        Public Property IsActive() As Object
            Get
                Return _IsActive
            End Get
            Set(ByVal value As Object)
                _IsActive = value
            End Set
        End Property

        Public Property ProjectCode() As Object
            Get
                Return _ProjectCode
            End Get
            Set(ByVal value As Object)
                _ProjectCode = value
            End Set
        End Property

        Public Property ParentSchemeID() As Object
            Get
                Return _ParentSchemeID
            End Get
            Set(ByVal value As Object)
                _ParentSchemeID = value
            End Set
        End Property

        Public Property StartDate() As Object
            Get
                Return _StartDate
            End Get
            Set(ByVal value As Object)
                _StartDate = value
            End Set
        End Property

        Public Property RiskGovernment() As Object
            Get
                Return _RiskGovernment
            End Get
            Set(ByVal value As Object)
                _RiskGovernment = value
            End Set
        End Property
        Public Property RiskType() As Object
            Get
                Return _RiskType
            End Get
            Set(ByVal value As Object)
                _RiskType = value
            End Set
        End Property

        Public Property PolType() As Object
            Get
                Return _PolType
            End Get
            Set(ByVal value As Object)
                _PolType = value
            End Set
        End Property

    End Class

    <Serializable()> _
    Public Class MailContract
        Private _ID As Object
        Private _Code As Object
        Private _Mailto As Object
        'Private _MailCC As Object
        Private _Name As Object
        Private _AccountNo As Object
        Private _Type As Object
        Private _M2Mailto As Object
        Private _M2MailCC As Object


        Public Property ID() As Object
            Get
                Return _ID
            End Get
            Set(ByVal value As Object)
                _ID = value
            End Set
        End Property

        Public Property AccountNo() As Object
            Get
                Return _AccountNo
            End Get
            Set(ByVal value As Object)
                _AccountNo = value
            End Set
        End Property

        Public Property Code() As Object
            Get
                Return _Code
            End Get
            Set(ByVal value As Object)
                _Code = value
            End Set
        End Property

        Public Property Mailto() As Object
            Get
                Return _Mailto
            End Get
            Set(ByVal value As Object)
                _Mailto = value
            End Set
        End Property

        'Public Property MailCC() As Object
        '    Get
        '        Return _MailCC
        '    End Get
        '    Set(ByVal value As Object)
        '        _MailCC = value
        '    End Set
        'End Property
        Public Property Name() As Object
            Get
                Return _Name
            End Get
            Set(ByVal value As Object)
                _Name = value
            End Set
        End Property
        Public Property Type() As Object
            Get
                Return _Type
            End Get
            Set(ByVal value As Object)
                _Type = value
            End Set
        End Property
        Public Property M2Mailto() As Object
            Get
                Return _M2Mailto
            End Get
            Set(ByVal value As Object)
                _M2Mailto = value
            End Set
        End Property
        Public Property M2MailCC() As Object
            Get
                Return _M2MailCC
            End Get
            Set(ByVal value As Object)
                _M2MailCC = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class NoticeHeader
        Private _NoticeID As Object
        Private _NoticeTitle As Object
        Private _CreationBy As Object
        Private _CreationDate As Object
        Private _NoticeCode As Object
        Private _NoticeDate As Object
        Private _DueDate As Object
        Private _ModifyBy As Object
        Private _ModifyDate As Object
        Private _SendBy As Object
        Private _SendDate As Object
        Private _ReSendBy As Object
        Private _ReSendDate As Object

        Public Property NoticeID() As Object
            Get
                Return _NoticeID
            End Get
            Set(ByVal value As Object)
                _NoticeID = value
            End Set
        End Property

        Public Property NoticeTitle() As Object
            Get
                Return _NoticeTitle
            End Get
            Set(ByVal value As Object)
                _NoticeTitle = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property

        Public Property ModifyDate() As Object
            Get
                Return _ModifyDate
            End Get
            Set(ByVal value As Object)
                _ModifyDate = value
            End Set
        End Property

        Public Property NoticeCode() As Object
            Get
                Return _NoticeCode
            End Get
            Set(ByVal value As Object)
                _NoticeCode = value
            End Set
        End Property

        Public Property SendDate() As Object
            Get
                Return _SendDate
            End Get
            Set(ByVal value As Object)
                _SendDate = value
            End Set
        End Property

        Public Property ReSendDate() As Object
            Get
                Return _ReSendDate
            End Get
            Set(ByVal value As Object)
                _ReSendDate = value
            End Set
        End Property
        Public Property NoticeDate() As Object
            Get
                Return _NoticeDate
            End Get
            Set(ByVal value As Object)
                _NoticeDate = value
            End Set
        End Property
        Public Property DueDate() As Object
            Get
                Return _DueDate
            End Get
            Set(ByVal value As Object)
                _DueDate = value
            End Set
        End Property
        Public Property CreationBy() As Object
            Get
                Return _CreationBy
            End Get
            Set(ByVal value As Object)
                _CreationBy = value
            End Set
        End Property
        Public Property ModifyBy() As Object
            Get
                Return _ModifyBy
            End Get
            Set(ByVal value As Object)
                _ModifyBy = value
            End Set
        End Property
        Public Property SendBy() As Object
            Get
                Return _SendBy
            End Get
            Set(ByVal value As Object)
                _SendBy = value
            End Set
        End Property
        Public Property ReSendBy() As Object
            Get
                Return _ReSendBy
            End Get
            Set(ByVal value As Object)
                _ReSendBy = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class V_Billing_Notification
        Private _NotificationID As Object
        Private _AgentCode As Object
        Private _AgentName As Object
        Private _Mailto As Object
        Private _WH As Object
        Private _Records As Object


        Public Property NotificationID() As Object
            Get
                Return _NotificationID
            End Get
            Set(ByVal value As Object)
                _NotificationID = value
            End Set
        End Property

        Public Property AgentCode() As Object
            Get
                Return _AgentCode
            End Get
            Set(ByVal value As Object)
                _AgentCode = value
            End Set
        End Property

        Public Property AgentName() As Object
            Get
                Return _AgentName
            End Get
            Set(ByVal value As Object)
                _AgentName = value
            End Set
        End Property

        Public Property Mailto() As Object
            Get
                Return _Mailto
            End Get
            Set(ByVal value As Object)
                _Mailto = value
            End Set
        End Property

        Public Property WH() As Object
            Get
                Return _WH
            End Get
            Set(ByVal value As Object)
                _WH = value
            End Set
        End Property

        Public Property Records() As Object
            Get
                Return _Records
            End Get
            Set(ByVal value As Object)
                _Records = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class Unwriter_Register
        Private _ID As Object
        Private _Underwriter As Object
        Private _CrossReference As Object
        Private _Name As Object
        Private _Address1 As Object
        Private _Address2 As Object
        Private _Address3 As Object
        Private _PostCode As Object
        Private _City As Object
        Private _PhoneBusiness As Object
        Private _PhoneHome As Object
        Private _Facsimile As Object
        Private _AccountContact As Object
        Private _Addressee As Object
        Private _Salutation As Object
        Private _DaysCredit As Object
        Private _TrueUnderwriter As Object
        Private _EntryBy As Object
        Private _EntryDate As Object
        Private _FinanceContact As Object
        Private _GeneralClaimContact As Object
        Private _Type As Object
        Private _InsuranceLine As Object
        Private _VATPayType As Object
        Private _PhoneFinance As Object
        Private _PhoneClaims As Object
        Private _FaxFinance As Object
        Private _FaxClaims As Object
        Private _CreationDate As Object
        Private _ApproveDate As Object
        Private _InsurerCode As Object

        Public Property ID() As Object
            Get
                Return _ID
            End Get
            Set(ByVal value As Object)
                _ID = value
            End Set
        End Property

        Public Property Underwriter() As Object
            Get
                Return _Underwriter
            End Get
            Set(ByVal value As Object)
                _Underwriter = value
            End Set
        End Property

        Public Property CrossReference() As Object
            Get
                Return _CrossReference
            End Get
            Set(ByVal value As Object)
                _CrossReference = value
            End Set
        End Property
        Public Property InsurerCode() As Object
            Get
                Return _InsurerCode
            End Get
            Set(ByVal value As Object)
                _InsurerCode = value
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

        Public Property Address1() As Object
            Get
                Return _Address1
            End Get
            Set(ByVal value As Object)
                _Address1 = value
            End Set
        End Property

        Public Property Address2() As Object
            Get
                Return _Address2
            End Get
            Set(ByVal value As Object)
                _Address2 = value
            End Set
        End Property

        Public Property Address3() As Object
            Get
                Return _Address3
            End Get
            Set(ByVal value As Object)
                _Address3 = value
            End Set
        End Property

        Public Property PostCode() As Object
            Get
                Return _PostCode
            End Get
            Set(ByVal value As Object)
                _PostCode = value
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

        Public Property PhoneBusiness() As Object
            Get
                Return _PhoneBusiness
            End Get
            Set(ByVal value As Object)
                _PhoneBusiness = value
            End Set
        End Property

        Public Property PhoneHome() As Object
            Get
                Return _PhoneHome
            End Get
            Set(ByVal value As Object)
                _PhoneHome = value
            End Set
        End Property

        Public Property Facsimile() As Object
            Get
                Return _Facsimile
            End Get
            Set(ByVal value As Object)
                _Facsimile = value
            End Set
        End Property

        Public Property AccountContact() As Object
            Get
                Return _AccountContact
            End Get
            Set(ByVal value As Object)
                _AccountContact = value
            End Set
        End Property

        Public Property Addressee() As Object
            Get
                Return _Addressee
            End Get
            Set(ByVal value As Object)
                _Addressee = value
            End Set
        End Property

        Public Property Salutation() As Object
            Get
                Return _Salutation
            End Get
            Set(ByVal value As Object)
                _Salutation = value
            End Set
        End Property

        Public Property DaysCredit() As Object
            Get
                Return _DaysCredit
            End Get
            Set(ByVal value As Object)
                _DaysCredit = value
            End Set
        End Property

        Public Property TrueUnderwriter() As Object
            Get
                Return _TrueUnderwriter
            End Get
            Set(ByVal value As Object)
                _TrueUnderwriter = value
            End Set
        End Property

        Public Property EntryBy() As Object
            Get
                Return _EntryBy
            End Get
            Set(ByVal value As Object)
                _EntryBy = value
            End Set
        End Property

        Public Property EntryDate() As Object
            Get
                Return _EntryDate
            End Get
            Set(ByVal value As Object)
                _EntryDate = value
            End Set
        End Property

        Public Property FinanceContact() As Object
            Get
                Return _FinanceContact
            End Get
            Set(ByVal value As Object)
                _FinanceContact = value
            End Set
        End Property

        Public Property GeneralClaimContact() As Object
            Get
                Return _GeneralClaimContact
            End Get
            Set(ByVal value As Object)
                _GeneralClaimContact = value
            End Set
        End Property

        Public Property Type() As Object
            Get
                Return _Type
            End Get
            Set(ByVal value As Object)
                _Type = value
            End Set
        End Property

        Public Property InsuranceLine() As Object
            Get
                Return _InsuranceLine
            End Get
            Set(ByVal value As Object)
                _InsuranceLine = value
            End Set
        End Property

        Public Property VATPayType() As Object
            Get
                Return _VATPayType
            End Get
            Set(ByVal value As Object)
                _VATPayType = value
            End Set
        End Property

        Public Property PhoneFinance() As Object
            Get
                Return _PhoneFinance
            End Get
            Set(ByVal value As Object)
                _PhoneFinance = value
            End Set
        End Property

        Public Property PhoneClaims() As Object
            Get
                Return _PhoneClaims
            End Get
            Set(ByVal value As Object)
                _PhoneClaims = value
            End Set
        End Property

        Public Property FaxFinance() As Object
            Get
                Return _FaxFinance
            End Get
            Set(ByVal value As Object)
                _FaxFinance = value
            End Set
        End Property

        Public Property FaxClaims() As Object
            Get
                Return _FaxClaims
            End Get
            Set(ByVal value As Object)
                _FaxClaims = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property

        Public Property ApproveDate() As Object
            Get
                Return _ApproveDate
            End Get
            Set(ByVal value As Object)
                _ApproveDate = value
            End Set
        End Property



    End Class

    <Serializable()> _
    Public Class Agent_Register
        Private _ID As Object
        Private _Agent As Object
        Private _Name As Object
        Private _Address1 As Object
        Private _Address2 As Object
        Private _Address3 As Object
        Private _PostCode As Object
        Private _City As Object
        Private _PhoneBusiness As Object
        Private _PhoneHome As Object
        Private _ContactPerson As Object
        Private _Addressee As Object
        Private _Salutation As Object
        Private _Occupation As Object
        Private _EntryBy As Object
        Private _EntryDate As Object
        Private _IsSubAgent As Object
        Private _InternetAddress As Object
        Private _CreationDate As Object
        Private _ApproveDate As Object

        Public Property ID() As Object
            Get
                Return _ID
            End Get
            Set(ByVal value As Object)
                _ID = value
            End Set
        End Property

        Public Property Agent() As Object
            Get
                Return _Agent
            End Get
            Set(ByVal value As Object)
                _Agent = value
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

        Public Property Address1() As Object
            Get
                Return _Address1
            End Get
            Set(ByVal value As Object)
                _Address1 = value
            End Set
        End Property

        Public Property Address2() As Object
            Get
                Return _Address2
            End Get
            Set(ByVal value As Object)
                _Address2 = value
            End Set
        End Property

        Public Property Address3() As Object
            Get
                Return _Address3
            End Get
            Set(ByVal value As Object)
                _Address3 = value
            End Set
        End Property

        Public Property PostCode() As Object
            Get
                Return _PostCode
            End Get
            Set(ByVal value As Object)
                _PostCode = value
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

        Public Property PhoneBusiness() As Object
            Get
                Return _PhoneBusiness
            End Get
            Set(ByVal value As Object)
                _PhoneBusiness = value
            End Set
        End Property

        Public Property PhoneHome() As Object
            Get
                Return _PhoneHome
            End Get
            Set(ByVal value As Object)
                _PhoneHome = value
            End Set
        End Property

        Public Property ContactPerson() As Object
            Get
                Return _ContactPerson
            End Get
            Set(ByVal value As Object)
                _ContactPerson = value
            End Set
        End Property

        Public Property Addressee() As Object
            Get
                Return _Addressee
            End Get
            Set(ByVal value As Object)
                _Addressee = value
            End Set
        End Property

        Public Property Salutation() As Object
            Get
                Return _Salutation
            End Get
            Set(ByVal value As Object)
                _Salutation = value
            End Set
        End Property

        Public Property Occupation() As Object
            Get
                Return _Occupation
            End Get
            Set(ByVal value As Object)
                _Occupation = value
            End Set
        End Property

        Public Property EntryBy() As Object
            Get
                Return _EntryBy
            End Get
            Set(ByVal value As Object)
                _EntryBy = value
            End Set
        End Property

        Public Property EntryDate() As Object
            Get
                Return _EntryDate
            End Get
            Set(ByVal value As Object)
                _EntryDate = value
            End Set
        End Property

        Public Property IsSubAgent() As Object
            Get
                Return _IsSubAgent
            End Get
            Set(ByVal value As Object)
                _IsSubAgent = value
            End Set
        End Property

        Public Property InternetAddress() As Object
            Get
                Return _InternetAddress
            End Get
            Set(ByVal value As Object)
                _InternetAddress = value
            End Set
        End Property

        Public Property CreationDate() As Object
            Get
                Return _CreationDate
            End Get
            Set(ByVal value As Object)
                _CreationDate = value
            End Set
        End Property

        Public Property ApproveDate() As Object
            Get
                Return _ApproveDate
            End Get
            Set(ByVal value As Object)
                _ApproveDate = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class V_InsuredCode
        Private _ID As Object
        Private _InsurerCode As Object
        Private _Underwriter As Object
        Private _CrossReference As Object
        Private _Name As Object
        Private _Address1 As Object
        Private _Address2 As Object
        Private _Address3 As Object
        Private _PostCode As Object
        Private _City As Object
        Private _PhoneBusiness As Object
        Private _PhoneHome As Object
        Private _Facsimile As Object
        Private _AccountContact As Object
        Private _Addressee As Object
        Private _Salutation As Object
        Private _DaysCredit As Object
        Private _TrueUnderwriter As Object
        Private _EntryBy As Object
        Private _EntryDate As Object
        Private _FinanceContact As Object
        Private _GeneralClaimContact As Object
        Private _Type As Object
        Private _InsuranceLine As Object
        Private _VATPayType As Object
        Private _PhoneFinance As Object
        Private _PhoneClaims As Object
        Private _FaxFinance As Object
        Private _FaxClaims As Object


        Public Property ID() As Object
            Get
                Return _ID
            End Get
            Set(ByVal value As Object)
                _ID = value
            End Set
        End Property

        Public Property InsurerCode() As Object
            Get
                Return _InsurerCode
            End Get
            Set(ByVal value As Object)
                _InsurerCode = value
            End Set
        End Property

        Public Property Underwriter() As Object
            Get
                Return _Underwriter
            End Get
            Set(ByVal value As Object)
                _Underwriter = value
            End Set
        End Property

        Public Property CrossReference() As Object
            Get
                Return _CrossReference
            End Get
            Set(ByVal value As Object)
                _CrossReference = value
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

        Public Property Address1() As Object
            Get
                Return _Address1
            End Get
            Set(ByVal value As Object)
                _Address1 = value
            End Set
        End Property

        Public Property Address2() As Object
            Get
                Return _Address2
            End Get
            Set(ByVal value As Object)
                _Address2 = value
            End Set
        End Property

        Public Property Address3() As Object
            Get
                Return _Address3
            End Get
            Set(ByVal value As Object)
                _Address3 = value
            End Set
        End Property

        Public Property PostCode() As Object
            Get
                Return _PostCode
            End Get
            Set(ByVal value As Object)
                _PostCode = value
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

        Public Property PhoneBusiness() As Object
            Get
                Return _PhoneBusiness
            End Get
            Set(ByVal value As Object)
                _PhoneBusiness = value
            End Set
        End Property

        Public Property PhoneHome() As Object
            Get
                Return _PhoneHome
            End Get
            Set(ByVal value As Object)
                _PhoneHome = value
            End Set
        End Property

        Public Property Facsimile() As Object
            Get
                Return _Facsimile
            End Get
            Set(ByVal value As Object)
                _Facsimile = value
            End Set
        End Property

        Public Property AccountContact() As Object
            Get
                Return _AccountContact
            End Get
            Set(ByVal value As Object)
                _AccountContact = value
            End Set
        End Property

        Public Property Addressee() As Object
            Get
                Return _Addressee
            End Get
            Set(ByVal value As Object)
                _Addressee = value
            End Set
        End Property

        Public Property Salutation() As Object
            Get
                Return _Salutation
            End Get
            Set(ByVal value As Object)
                _Salutation = value
            End Set
        End Property

        Public Property DaysCredit() As Object
            Get
                Return _DaysCredit
            End Get
            Set(ByVal value As Object)
                _DaysCredit = value
            End Set
        End Property

        Public Property TrueUnderwriter() As Object
            Get
                Return _TrueUnderwriter
            End Get
            Set(ByVal value As Object)
                _TrueUnderwriter = value
            End Set
        End Property

        Public Property EntryBy() As Object
            Get
                Return _EntryBy
            End Get
            Set(ByVal value As Object)
                _EntryBy = value
            End Set
        End Property

        Public Property EntryDate() As Object
            Get
                Return _EntryDate
            End Get
            Set(ByVal value As Object)
                _EntryDate = value
            End Set
        End Property

        Public Property FinanceContact() As Object
            Get
                Return _FinanceContact
            End Get
            Set(ByVal value As Object)
                _FinanceContact = value
            End Set
        End Property

        Public Property GeneralClaimContact() As Object
            Get
                Return _GeneralClaimContact
            End Get
            Set(ByVal value As Object)
                _GeneralClaimContact = value
            End Set
        End Property

        Public Property Type() As Object
            Get
                Return _Type
            End Get
            Set(ByVal value As Object)
                _Type = value
            End Set
        End Property

        Public Property InsuranceLine() As Object
            Get
                Return _InsuranceLine
            End Get
            Set(ByVal value As Object)
                _InsuranceLine = value
            End Set
        End Property

        Public Property VATPayType() As Object
            Get
                Return _VATPayType
            End Get
            Set(ByVal value As Object)
                _VATPayType = value
            End Set
        End Property

        Public Property PhoneFinance() As Object
            Get
                Return _PhoneFinance
            End Get
            Set(ByVal value As Object)
                _PhoneFinance = value
            End Set
        End Property

        Public Property PhoneClaims() As Object
            Get
                Return _PhoneClaims
            End Get
            Set(ByVal value As Object)
                _PhoneClaims = value
            End Set
        End Property

        Public Property FaxFinance() As Object
            Get
                Return _FaxFinance
            End Get
            Set(ByVal value As Object)
                _FaxFinance = value
            End Set
        End Property

        Public Property FaxClaims() As Object
            Get
                Return _FaxClaims
            End Get
            Set(ByVal value As Object)
                _FaxClaims = value
            End Set
        End Property



    End Class

    <Serializable()> _
    Public Class V_CarPrice
        Private _ID As Object
        Private _CarName As Object
        Private _CarCode As Object
        Private _CarType As Object
        Private _CarGroup As Object
        Private _CarBodyType As Object
        Private _CarCC As Object
        Private _CarFuel As Object
        Private _CarWeight As Object
        Private _CarSeat As Object
        Private _CarPrice As Object
        Private _CarYear As Object
        Private _IsActive As Object
        Private _PrefixChassis As Object
        Private _PrefixEngine As Object
        Private _Name As Object
        Private _CarTypeName As Object
        Private _ModelID As Object

        Public Property ID() As Object
            Get
                Return _ID
            End Get
            Set(ByVal value As Object)
                _ID = value
            End Set
        End Property

        Public Property CarName() As Object
            Get
                Return _CarName
            End Get
            Set(ByVal value As Object)
                _CarName = value
            End Set
        End Property

        Public Property CarCode() As Object
            Get
                Return _CarCode
            End Get
            Set(ByVal value As Object)
                _CarCode = value
            End Set
        End Property

        Public Property CarType() As Object
            Get
                Return _CarType
            End Get
            Set(ByVal value As Object)
                _CarType = value
            End Set
        End Property

        Public Property CarGroup() As Object
            Get
                Return _CarGroup
            End Get
            Set(ByVal value As Object)
                _CarGroup = value
            End Set
        End Property

        Public Property CarBodyType() As Object
            Get
                Return _CarBodyType
            End Get
            Set(ByVal value As Object)
                _CarBodyType = value
            End Set
        End Property

        Public Property CarCC() As Object
            Get
                Return _CarCC
            End Get
            Set(ByVal value As Object)
                _CarCC = value
            End Set
        End Property

        Public Property CarFuel() As Object
            Get
                Return _CarFuel
            End Get
            Set(ByVal value As Object)
                _CarFuel = value
            End Set
        End Property

        Public Property CarWeight() As Object
            Get
                Return _CarWeight
            End Get
            Set(ByVal value As Object)
                _CarWeight = value
            End Set
        End Property

        Public Property CarSeat() As Object
            Get
                Return _CarSeat
            End Get
            Set(ByVal value As Object)
                _CarSeat = value
            End Set
        End Property

        Public Property CarPrice() As Object
            Get
                Return _CarPrice
            End Get
            Set(ByVal value As Object)
                _CarPrice = value
            End Set
        End Property

        Public Property CarYear() As Object
            Get
                Return _CarYear
            End Get
            Set(ByVal value As Object)
                _CarYear = value
            End Set
        End Property

        Public Property IsActive() As Object
            Get
                Return _IsActive
            End Get
            Set(ByVal value As Object)
                _IsActive = value
            End Set
        End Property

        Public Property PrefixChassis() As Object
            Get
                Return _PrefixChassis
            End Get
            Set(ByVal value As Object)
                _PrefixChassis = value
            End Set
        End Property

        Public Property PrefixEngine() As Object
            Get
                Return _PrefixEngine
            End Get
            Set(ByVal value As Object)
                _PrefixEngine = value
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

        Public Property CarTypeName() As Object
            Get
                Return _CarTypeName
            End Get
            Set(ByVal value As Object)
                _CarTypeName = value
            End Set
        End Property

        Public Property ModelID() As Object
            Get
                Return _ModelID
            End Get
            Set(ByVal value As Object)
                _ModelID = value
            End Set
        End Property


    End Class

    <Serializable()> _
    Public Class V_CAR_PREMIUM
        Private _PID As Object
        Private _ProductName As Object
        Private _CampaignName As Object
        Private _Uwriter As Object
        Private _UnwriterName As Object
        Private _AccountContact As Object
        Private _CarBrand As Object
        Private _CarModel As Object
        Private _PolicyType As Object
        Private _PremiumName As Object
        Private _CarPrice As Object
        Private _SumInsuredFrom As Object
        Private _SumInsuredTo As Object
        Private _TotalPremium As Object
        Private _DeductValue As Object
        Private _HasFloodValue As Object
        Private _HasCMI As Object
        Private _HasDriver As Object
        Private _CarGroup As Object
        Private _CarRegType As Object
        Private _CarAgeFrom As Object
        Private _CarAgeTo As Object
        Private _CarYear As Object
        Private _GarageType As Object
        Private _CarType As Object
        Private _PremiumID As Object
        Private _CampaignID As Object
        Private _CarBrandModelID As Object
        Private _RiskUwriterID As Object
        Private _PremiumIsActive As Object
        Private _CampaignIsActive As Object
        Private _CampaignStartDate As Object
        Private _CampaignExpiryDate As Object
        Private _CampaignPublishDate As Object
        Private _CampaignPublishBy As Object
        Private _PremiumModelCode As Object
        Private _GrossPremium As Object
        Private _Stamp As Object
        Private _vat As Object
        Private _NetPremium As Object
        Private _DiscountAmt As Object
        Private _Brokerage As Object
        Private _ORCommissionPercent As Object
        Private _AdminFeeIn1 As Object
        Private _AdminFeeIn2 As Object
        Private _WH As Object
        Private _LWT As Object
        Private _DLR As Object
        Private _TPPDValue As Object
        Private _TPBITimeValue As Object
        Private _TPBIPersonValue As Object
        Private _PAValue As Object
        Private _MedicalValue As Object
        Private _BailBondValue As Object
        Private _PersonValue As Object



        Public Property PID() As Object
            Get
                Return _PID
            End Get
            Set(ByVal value As Object)
                _PID = value
            End Set
        End Property

        Public Property ProductName() As Object
            Get
                Return _ProductName
            End Get
            Set(ByVal value As Object)
                _ProductName = value
            End Set
        End Property

        Public Property Uwriter() As Object
            Get
                Return _Uwriter
            End Get
            Set(ByVal value As Object)
                _Uwriter = value
            End Set
        End Property
        Public Property CampaignName() As Object
            Get
                Return _CampaignName
            End Get
            Set(ByVal value As Object)
                _CampaignName = value
            End Set
        End Property

        Public Property AccountContact() As Object
            Get
                Return _AccountContact
            End Get
            Set(ByVal value As Object)
                _AccountContact = value
            End Set
        End Property
        Public Property UnwriterName() As Object
            Get
                Return _UnwriterName
            End Get
            Set(ByVal value As Object)
                _UnwriterName = value
            End Set
        End Property

        Public Property CarBrand() As Object
            Get
                Return _CarBrand
            End Get
            Set(ByVal value As Object)
                _CarBrand = value
            End Set
        End Property

        Public Property CarModel() As Object
            Get
                Return _CarModel
            End Get
            Set(ByVal value As Object)
                _CarModel = value
            End Set
        End Property

        Public Property PolicyType() As Object
            Get
                Return _PolicyType
            End Get
            Set(ByVal value As Object)
                _PolicyType = value
            End Set
        End Property

        Public Property PremiumName() As Object
            Get
                Return _PremiumName
            End Get
            Set(ByVal value As Object)
                _PremiumName = value
            End Set
        End Property

        Public Property CarPrice() As Object
            Get
                Return _CarPrice
            End Get
            Set(ByVal value As Object)
                _CarPrice = value
            End Set
        End Property

        Public Property SumInsuredFrom() As Object
            Get
                Return _SumInsuredFrom
            End Get
            Set(ByVal value As Object)
                _SumInsuredFrom = value
            End Set
        End Property

        Public Property SumInsuredTo() As Object
            Get
                Return _SumInsuredTo
            End Get
            Set(ByVal value As Object)
                _SumInsuredTo = value
            End Set
        End Property

        Public Property TotalPremium() As Object
            Get
                Return _TotalPremium
            End Get
            Set(ByVal value As Object)
                _TotalPremium = value
            End Set
        End Property

        Public Property DeductValue() As Object
            Get
                Return _DeductValue
            End Get
            Set(ByVal value As Object)
                _DeductValue = value
            End Set
        End Property

        Public Property HasFloodValue() As Object
            Get
                Return _HasFloodValue
            End Get
            Set(ByVal value As Object)
                _HasFloodValue = value
            End Set
        End Property

        Public Property HasCMI() As Object
            Get
                Return _HasCMI
            End Get
            Set(ByVal value As Object)
                _HasCMI = value
            End Set
        End Property

        Public Property HasDriver() As Object
            Get
                Return _HasDriver
            End Get
            Set(ByVal value As Object)
                _HasDriver = value
            End Set
        End Property

        Public Property CarGroup() As Object
            Get
                Return _CarGroup
            End Get
            Set(ByVal value As Object)
                _CarGroup = value
            End Set
        End Property

        Public Property CarRegType() As Object
            Get
                Return _CarRegType
            End Get
            Set(ByVal value As Object)
                _CarRegType = value
            End Set
        End Property

        Public Property CarAgeFrom() As Object
            Get
                Return _CarAgeFrom
            End Get
            Set(ByVal value As Object)
                _CarAgeFrom = value
            End Set
        End Property

        Public Property CarAgeTo() As Object
            Get
                Return _CarAgeTo
            End Get
            Set(ByVal value As Object)
                _CarAgeTo = value
            End Set
        End Property

        Public Property CarYear() As Object
            Get
                Return _CarYear
            End Get
            Set(ByVal value As Object)
                _CarYear = value
            End Set
        End Property

        Public Property GarageType() As Object
            Get
                Return _GarageType
            End Get
            Set(ByVal value As Object)
                _GarageType = value
            End Set
        End Property

        Public Property CarType() As Object
            Get
                Return _CarType
            End Get
            Set(ByVal value As Object)
                _CarType = value
            End Set
        End Property

        Public Property PremiumID() As Object
            Get
                Return _PremiumID
            End Get
            Set(ByVal value As Object)
                _PremiumID = value
            End Set
        End Property

        Public Property CampaignID() As Object
            Get
                Return _CampaignID
            End Get
            Set(ByVal value As Object)
                _CampaignID = value
            End Set
        End Property

        Public Property CarBrandModelID() As Object
            Get
                Return _CarBrandModelID
            End Get
            Set(ByVal value As Object)
                _CarBrandModelID = value
            End Set
        End Property

        Public Property RiskUwriterID() As Object
            Get
                Return _RiskUwriterID
            End Get
            Set(ByVal value As Object)
                _RiskUwriterID = value
            End Set
        End Property

        Public Property PremiumIsActive() As Object
            Get
                Return _PremiumIsActive
            End Get
            Set(ByVal value As Object)
                _PremiumIsActive = value
            End Set
        End Property

        Public Property CampaignIsActive() As Object
            Get
                Return _CampaignIsActive
            End Get
            Set(ByVal value As Object)
                _CampaignIsActive = value
            End Set
        End Property

        Public Property CampaignStartDate() As Object
            Get
                Return _CampaignStartDate
            End Get
            Set(ByVal value As Object)
                _CampaignStartDate = value
            End Set
        End Property

        Public Property CampaignExpiryDate() As Object
            Get
                Return _CampaignExpiryDate
            End Get
            Set(ByVal value As Object)
                _CampaignExpiryDate = value
            End Set
        End Property

        Public Property CampaignPublishDate() As Object
            Get
                Return _CampaignPublishDate
            End Get
            Set(ByVal value As Object)
                _CampaignPublishDate = value
            End Set
        End Property

        Public Property CampaignPublishBy() As Object
            Get
                Return _CampaignPublishBy
            End Get
            Set(ByVal value As Object)
                _CampaignPublishBy = value
            End Set
        End Property

        Public Property PremiumModelCode() As Object
            Get
                Return _PremiumModelCode
            End Get
            Set(ByVal value As Object)
                _PremiumModelCode = value
            End Set
        End Property

        Public Property GrossPremium() As Object
            Get
                Return _GrossPremium
            End Get
            Set(ByVal value As Object)
                _GrossPremium = value
            End Set
        End Property

        Public Property Stamp() As Object
            Get
                Return _Stamp
            End Get
            Set(ByVal value As Object)
                _Stamp = value
            End Set
        End Property

        Public Property vat() As Object
            Get
                Return _vat
            End Get
            Set(ByVal value As Object)
                _vat = value
            End Set
        End Property

        Public Property NetPremium() As Object
            Get
                Return _NetPremium
            End Get
            Set(ByVal value As Object)
                _NetPremium = value
            End Set
        End Property

        Public Property DiscountAmt() As Object
            Get
                Return _DiscountAmt
            End Get
            Set(ByVal value As Object)
                _DiscountAmt = value
            End Set
        End Property

        Public Property Brokerage() As Object
            Get
                Return _Brokerage
            End Get
            Set(ByVal value As Object)
                _Brokerage = value
            End Set
        End Property

        Public Property ORCommissionPercent() As Object
            Get
                Return _ORCommissionPercent
            End Get
            Set(ByVal value As Object)
                _ORCommissionPercent = value
            End Set
        End Property

        Public Property AdminFeeIn1() As Object
            Get
                Return _AdminFeeIn1
            End Get
            Set(ByVal value As Object)
                _AdminFeeIn1 = value
            End Set
        End Property

        Public Property AdminFeeIn2() As Object
            Get
                Return _AdminFeeIn2
            End Get
            Set(ByVal value As Object)
                _AdminFeeIn2 = value
            End Set
        End Property

        Public Property WH() As Object
            Get
                Return _WH
            End Get
            Set(ByVal value As Object)
                _WH = value
            End Set
        End Property

        Public Property LWT() As Object
            Get
                Return _LWT
            End Get
            Set(ByVal value As Object)
                _LWT = value
            End Set
        End Property

        Public Property DLR() As Object
            Get
                Return _DLR
            End Get
            Set(ByVal value As Object)
                _DLR = value
            End Set
        End Property
        Public Property TPPDValue() As Object
            Get
                Return _TPPDValue
            End Get
            Set(ByVal value As Object)
                _TPPDValue = value
            End Set
        End Property

        Public Property TPBITimeValue() As Object
            Get
                Return _TPBITimeValue
            End Get
            Set(ByVal value As Object)
                _TPBITimeValue = value
            End Set
        End Property

        Public Property TPBIPersonValue() As Object
            Get
                Return _TPBIPersonValue
            End Get
            Set(ByVal value As Object)
                _TPBIPersonValue = value
            End Set
        End Property

        Public Property PAValue() As Object
            Get
                Return _PAValue
            End Get
            Set(ByVal value As Object)
                _PAValue = value
            End Set
        End Property

        Public Property MedicalValue() As Object
            Get
                Return _MedicalValue
            End Get
            Set(ByVal value As Object)
                _MedicalValue = value
            End Set
        End Property

        Public Property BailBondValue() As Object
            Get
                Return _BailBondValue
            End Get
            Set(ByVal value As Object)
                _BailBondValue = value
            End Set
        End Property

        Public Property PersonValue() As Object
            Get
                Return _PersonValue
            End Get
            Set(ByVal value As Object)
                _PersonValue = value
            End Set
        End Property



    End Class

    <Serializable()> _
    Public Class V_NLTBooking
        Private _AID As Object
        Private _ClientCode As Object
        Private _ClientName As Object
        Private _TempID As Object
        Private _StartingDate As Object
        Private _InsurerNameThai As Object
        Private _ChassisNo As Object
        Private _EngineNo As Object
        Private _NetVoluntaryPremium As Object
        Private _SumInsured As Object
        Private _VoluntaryPremium As Object
        Private _CMIPremium As Object
        Private _LoanContractNo As Object
        Private _Status As Object
        Private _Remark As Object
        Private _VMIPolicy As Object
        Private _CMIPolicy As Object
        Private _Showroom As Object
        Private _Beneficiary As Object
        Private _ContactPerson As Object
        Private _DateIn As Object
        Private _Issues As Object
        Private _BillingName As Object
        Private _BillingAddress As Object
        Private _PolicyNew As Object


        Public Property AID() As Object
            Get
                Return _AID
            End Get
            Set(ByVal value As Object)
                _AID = value
            End Set
        End Property

        Public Property ClientCode() As Object
            Get
                Return _ClientCode
            End Get
            Set(ByVal value As Object)
                _ClientCode = value
            End Set
        End Property

        Public Property ClientName() As Object
            Get
                Return _ClientName
            End Get
            Set(ByVal value As Object)
                _ClientName = value
            End Set
        End Property

        Public Property TempID() As Object
            Get
                Return _TempID
            End Get
            Set(ByVal value As Object)
                _TempID = value
            End Set
        End Property

        Public Property StartingDate() As Object
            Get
                Return _StartingDate
            End Get
            Set(ByVal value As Object)
                _StartingDate = value
            End Set
        End Property

        Public Property InsurerNameThai() As Object
            Get
                Return _InsurerNameThai
            End Get
            Set(ByVal value As Object)
                _InsurerNameThai = value
            End Set
        End Property

        Public Property ChassisNo() As Object
            Get
                Return _ChassisNo
            End Get
            Set(ByVal value As Object)
                _ChassisNo = value
            End Set
        End Property

        Public Property EngineNo() As Object
            Get
                Return _EngineNo
            End Get
            Set(ByVal value As Object)
                _EngineNo = value
            End Set
        End Property

        Public Property NetVoluntaryPremium() As Object
            Get
                Return _NetVoluntaryPremium
            End Get
            Set(ByVal value As Object)
                _NetVoluntaryPremium = value
            End Set
        End Property

        Public Property SumInsured() As Object
            Get
                Return _SumInsured
            End Get
            Set(ByVal value As Object)
                _SumInsured = value
            End Set
        End Property

        Public Property VoluntaryPremium() As Object
            Get
                Return _VoluntaryPremium
            End Get
            Set(ByVal value As Object)
                _VoluntaryPremium = value
            End Set
        End Property

        Public Property CMIPremium() As Object
            Get
                Return _CMIPremium
            End Get
            Set(ByVal value As Object)
                _CMIPremium = value
            End Set
        End Property

        Public Property LoanContractNo() As Object
            Get
                Return _LoanContractNo
            End Get
            Set(ByVal value As Object)
                _LoanContractNo = value
            End Set
        End Property

        Public Property Status() As Object
            Get
                Return _Status
            End Get
            Set(ByVal value As Object)
                _Status = value
            End Set
        End Property

        Public Property Remark() As Object
            Get
                Return _Remark
            End Get
            Set(ByVal value As Object)
                _Remark = value
            End Set
        End Property

        Public Property VMIPolicy() As Object
            Get
                Return _VMIPolicy
            End Get
            Set(ByVal value As Object)
                _VMIPolicy = value
            End Set
        End Property

        Public Property CMIPolicy() As Object
            Get
                Return _CMIPolicy
            End Get
            Set(ByVal value As Object)
                _CMIPolicy = value
            End Set
        End Property

        Public Property Showroom() As Object
            Get
                Return _Showroom
            End Get
            Set(ByVal value As Object)
                _Showroom = value
            End Set
        End Property

        Public Property Beneficiary() As Object
            Get
                Return _Beneficiary
            End Get
            Set(ByVal value As Object)
                _Beneficiary = value
            End Set
        End Property

        Public Property ContactPerson() As Object
            Get
                Return _ContactPerson
            End Get
            Set(ByVal value As Object)
                _ContactPerson = value
            End Set
        End Property

        Public Property DateIn() As Object
            Get
                Return _DateIn
            End Get
            Set(ByVal value As Object)
                _DateIn = value
            End Set
        End Property

        Public Property Issues() As Object
            Get
                Return _Issues
            End Get
            Set(ByVal value As Object)
                _Issues = value
            End Set
        End Property

        Public Property BillingName() As Object
            Get
                Return _BillingName
            End Get
            Set(ByVal value As Object)
                _BillingName = value
            End Set
        End Property

        Public Property BillingAddress() As Object
            Get
                Return _BillingAddress
            End Get
            Set(ByVal value As Object)
                _BillingAddress = value
            End Set
        End Property
        Public Property PolicyNew() As Object
            Get
                Return _PolicyNew
            End Get
            Set(ByVal value As Object)
                _PolicyNew = value
            End Set
        End Property

    End Class

    <Serializable()> _
    Public Class ImportMotorAppForm316
        Private _RowNo As Object
        Private _ReferenceNo As Object
        Private _Title As Object
        Private _Name As Object
        Private _SurName As Object
        Private _LoanContractNo As Object
        Private _Address1 As Object
        Private _Address2 As Object
        Private _Mobile As Object
        Private _OfficePhone As Object
        Private _HomePhone As Object
        Private _Insurer As Object
        Private _TypeInsurance As Object
        Private _CarPrice As Object
        Private _Brand As Object
        Private _Model As Object
        Private _ModelCode As Object
        Private _YearOfCar As Object
        Private _CC As Object
        Private _Seat As Object
        Private _Weight As Object
        Private _EngineNo As Object
        Private _ChassisNo As Object
        Private _Colour As Object
        Private _LicenseNo As Object
        Private _RegisterdName As Object
        Private _UseOfCar As Object
        Private _SumInsured As Object
        Private _StartingDate As Object
        Private _Showroom As Object
        Private _ShowroomCode As Object
        Private _ShowroomContactName As Object
        Private _ShowroomContactPhone As Object
        Private _CMIStatus As Object
        Private _Leasing As Object
        Private _Beneficiary As Object
        Private _Agent As Object
        Private _CoverageType As Object
        Private _BillingName1 As Object
        Private _BillingAddress1 As Object
        Private _BillingTotals1 As Object
        Private _BillingName2 As Object
        Private _BillingAddress2 As Object
        Private _BillingTotals2 As Object
        Private _TempID As Object
        Private _BatchNo As Object
        Private _IDCard As Object

        Public Property RowNo() As Object
            Get
                Return _RowNo
            End Get
            Set(ByVal value As Object)
                _RowNo = value
            End Set
        End Property

        Public Property ReferenceNo() As Object
            Get
                Return _ReferenceNo
            End Get
            Set(ByVal value As Object)
                _ReferenceNo = value
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

        Public Property Name() As Object
            Get
                Return _Name
            End Get
            Set(ByVal value As Object)
                _Name = value
            End Set
        End Property

        Public Property SurName() As Object
            Get
                Return _SurName
            End Get
            Set(ByVal value As Object)
                _SurName = value
            End Set
        End Property

        Public Property LoanContractNo() As Object
            Get
                Return _LoanContractNo
            End Get
            Set(ByVal value As Object)
                _LoanContractNo = value
            End Set
        End Property

        Public Property Address1() As Object
            Get
                Return _Address1
            End Get
            Set(ByVal value As Object)
                _Address1 = value
            End Set
        End Property

        Public Property Address2() As Object
            Get
                Return _Address2
            End Get
            Set(ByVal value As Object)
                _Address2 = value
            End Set
        End Property

        Public Property Mobile() As Object
            Get
                Return _Mobile
            End Get
            Set(ByVal value As Object)
                _Mobile = value
            End Set
        End Property

        Public Property OfficePhone() As Object
            Get
                Return _OfficePhone
            End Get
            Set(ByVal value As Object)
                _OfficePhone = value
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

        Public Property Insurer() As Object
            Get
                Return _Insurer
            End Get
            Set(ByVal value As Object)
                _Insurer = value
            End Set
        End Property

        Public Property TypeInsurance() As Object
            Get
                Return _TypeInsurance
            End Get
            Set(ByVal value As Object)
                _TypeInsurance = value
            End Set
        End Property

        Public Property CarPrice() As Object
            Get
                Return _CarPrice
            End Get
            Set(ByVal value As Object)
                _CarPrice = value
            End Set
        End Property

        Public Property Brand() As Object
            Get
                Return _Brand
            End Get
            Set(ByVal value As Object)
                _Brand = value
            End Set
        End Property

        Public Property Model() As Object
            Get
                Return _Model
            End Get
            Set(ByVal value As Object)
                _Model = value
            End Set
        End Property

        Public Property ModelCode() As Object
            Get
                Return _ModelCode
            End Get
            Set(ByVal value As Object)
                _ModelCode = value
            End Set
        End Property

        Public Property YearOfCar() As Object
            Get
                Return _YearOfCar
            End Get
            Set(ByVal value As Object)
                _YearOfCar = value
            End Set
        End Property

        Public Property CC() As Object
            Get
                Return _CC
            End Get
            Set(ByVal value As Object)
                _CC = value
            End Set
        End Property

        Public Property Seat() As Object
            Get
                Return _Seat
            End Get
            Set(ByVal value As Object)
                _Seat = value
            End Set
        End Property

        Public Property Weight() As Object
            Get
                Return _Weight
            End Get
            Set(ByVal value As Object)
                _Weight = value
            End Set
        End Property

        Public Property EngineNo() As Object
            Get
                Return _EngineNo
            End Get
            Set(ByVal value As Object)
                _EngineNo = value
            End Set
        End Property

        Public Property ChassisNo() As Object
            Get
                Return _ChassisNo
            End Get
            Set(ByVal value As Object)
                _ChassisNo = value
            End Set
        End Property

        Public Property Colour() As Object
            Get
                Return _Colour
            End Get
            Set(ByVal value As Object)
                _Colour = value
            End Set
        End Property

        Public Property LicenseNo() As Object
            Get
                Return _LicenseNo
            End Get
            Set(ByVal value As Object)
                _LicenseNo = value
            End Set
        End Property

        Public Property RegisterdName() As Object
            Get
                Return _RegisterdName
            End Get
            Set(ByVal value As Object)
                _RegisterdName = value
            End Set
        End Property

        Public Property UseOfCar() As Object
            Get
                Return _UseOfCar
            End Get
            Set(ByVal value As Object)
                _UseOfCar = value
            End Set
        End Property

        Public Property SumInsured() As Object
            Get
                Return _SumInsured
            End Get
            Set(ByVal value As Object)
                _SumInsured = value
            End Set
        End Property

        Public Property StartingDate() As Object
            Get
                Return _StartingDate
            End Get
            Set(ByVal value As Object)
                _StartingDate = value
            End Set
        End Property

        Public Property Showroom() As Object
            Get
                Return _Showroom
            End Get
            Set(ByVal value As Object)
                _Showroom = value
            End Set
        End Property

        Public Property ShowroomCode() As Object
            Get
                Return _ShowroomCode
            End Get
            Set(ByVal value As Object)
                _ShowroomCode = value
            End Set
        End Property

        Public Property ShowroomContactName() As Object
            Get
                Return _ShowroomContactName
            End Get
            Set(ByVal value As Object)
                _ShowroomContactName = value
            End Set
        End Property

        Public Property ShowroomContactPhone() As Object
            Get
                Return _ShowroomContactPhone
            End Get
            Set(ByVal value As Object)
                _ShowroomContactPhone = value
            End Set
        End Property

        Public Property CMIStatus() As Object
            Get
                Return _CMIStatus
            End Get
            Set(ByVal value As Object)
                _CMIStatus = value
            End Set
        End Property

        Public Property Leasing() As Object
            Get
                Return _Leasing
            End Get
            Set(ByVal value As Object)
                _Leasing = value
            End Set
        End Property

        Public Property Beneficiary() As Object
            Get
                Return _Beneficiary
            End Get
            Set(ByVal value As Object)
                _Beneficiary = value
            End Set
        End Property

        Public Property Agent() As Object
            Get
                Return _Agent
            End Get
            Set(ByVal value As Object)
                _Agent = value
            End Set
        End Property

        Public Property CoverageType() As Object
            Get
                Return _CoverageType
            End Get
            Set(ByVal value As Object)
                _CoverageType = value
            End Set
        End Property

        Public Property BillingName1() As Object
            Get
                Return _BillingName1
            End Get
            Set(ByVal value As Object)
                _BillingName1 = value
            End Set
        End Property

        Public Property BillingAddress1() As Object
            Get
                Return _BillingAddress1
            End Get
            Set(ByVal value As Object)
                _BillingAddress1 = value
            End Set
        End Property

        Public Property BillingTotals1() As Object
            Get
                Return _BillingTotals1
            End Get
            Set(ByVal value As Object)
                _BillingTotals1 = value
            End Set
        End Property

        Public Property BillingName2() As Object
            Get
                Return _BillingName2
            End Get
            Set(ByVal value As Object)
                _BillingName2 = value
            End Set
        End Property

        Public Property BillingAddress2() As Object
            Get
                Return _BillingAddress2
            End Get
            Set(ByVal value As Object)
                _BillingAddress2 = value
            End Set
        End Property

        Public Property BillingTotals2() As Object
            Get
                Return _BillingTotals2
            End Get
            Set(ByVal value As Object)
                _BillingTotals2 = value
            End Set
        End Property

        Public Property TempID() As Object
            Get
                Return _TempID
            End Get
            Set(ByVal value As Object)
                _TempID = value
            End Set
        End Property

        Public Property BatchNo() As Object
            Get
                Return _BatchNo
            End Get
            Set(ByVal value As Object)
                _BatchNo = value
            End Set
        End Property

        Public Property IDCard() As Object
            Get
                Return _IDCard
            End Get
            Set(ByVal value As Object)
                _IDCard = value
            End Set
        End Property


    End Class




    <Serializable()> _
    Public Class ImportMNT2CSV
        Private _InsuranceCode As Object
        Private _Model_Code As Object
        'Private _Retail_MONTH As Object
        Private _Memo As Object

        Private _Invoice_Tax_No As Object
        Private _Invoice_Tax_date As Object
        Private _Gross_Premium As Object
        Private _Stamp As Object
        Private _VAT As Object
        Private _Discount As Object
        Private _CampaignPremium As Object
        Private _Flag As Object
        Public Property InsuranceCode() As Object
            Get
                Return _InsuranceCode
            End Get
            Set(ByVal value As Object)
                _InsuranceCode = value
            End Set
        End Property

        Public Property Model_Code() As Object
            Get
                Return _Model_Code
            End Get
            Set(ByVal value As Object)
                _Model_Code = value
            End Set
        End Property
        'Public Property Retail_MONTH() As Object
        '    Get
        '        Return _Retail_MONTH
        '    End Get
        '    Set(ByVal value As Object)
        '        _Retail_MONTH = value
        '    End Set
        'End Property
        Public Property Memo() As Object
            Get
                Return _Memo
            End Get
            Set(ByVal value As Object)
                _Memo = value
            End Set
        End Property
        Public Property Invoice_Tax_No() As Object
            Get
                Return _Invoice_Tax_No
            End Get
            Set(ByVal value As Object)
                _Invoice_Tax_No = value
            End Set
        End Property
        Public Property Invoice_Tax_date() As Object
            Get
                Return _Invoice_Tax_date
            End Get
            Set(ByVal value As Object)
                _Invoice_Tax_date = value
            End Set
        End Property
        Public Property Gross_Premium() As Object
            Get
                Return _Gross_Premium
            End Get
            Set(ByVal value As Object)
                _Gross_Premium = value
            End Set
        End Property
        Public Property Stamp() As Object
            Get
                Return _Stamp
            End Get
            Set(ByVal value As Object)
                _Stamp = value
            End Set
        End Property
        Public Property VAT() As Object
            Get
                Return _VAT
            End Get
            Set(ByVal value As Object)
                _VAT = value
            End Set
        End Property
        Public Property Discount() As Object
            Get
                Return _Discount
            End Get
            Set(ByVal value As Object)
                _Discount = value
            End Set
        End Property
        Public Property CampaignPremium() As Object
            Get
                Return _CampaignPremium
            End Get
            Set(ByVal value As Object)
                _CampaignPremium = value
            End Set
        End Property

        Public Property Flag() As Object
            Get
                Return _Flag
            End Get
            Set(ByVal value As Object)
                _Flag = value
            End Set
        End Property
    End Class
End Namespace


