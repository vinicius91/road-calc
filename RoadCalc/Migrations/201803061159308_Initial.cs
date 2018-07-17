namespace RoadCalc.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Azimute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        PontoNotavelId = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        ProjetoId = c.Int(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .ForeignKey("dbo.PontoNotavel", t => t.PontoNotavelId)
                .Index(t => t.PontoNotavelId)
                .Index(t => t.ProjetoId);
            
            CreateTable(
                "dbo.PontoNotavel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        CoordenadaId = c.Int(nullable: false),
                        ProjetoId = c.Int(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordenada", t => t.CoordenadaId)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .Index(t => t.CoordenadaId)
                .Index(t => t.ProjetoId);
            
            CreateTable(
                "dbo.Coordenada",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Lat = c.Double(nullable: false),
                        Lng = c.Double(nullable: false),
                        X = c.Double(nullable: false),
                        Y = c.Double(nullable: false),
                        Z = c.Double(nullable: false),
                        ZoneLetter = c.String(unicode: false),
                        ZoneNumber = c.Int(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Projeto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClasseDeProjetoId = c.Int(nullable: false),
                        PontoInicialId = c.Int(nullable: false),
                        PontoFinalId = c.Int(nullable: false),
                        Nome = c.String(unicode: false),
                        FaixaDeDominio = c.Double(nullable: false),
                        UserId = c.Int(nullable: false),
                        CoordenadasReais = c.Boolean(nullable: false),
                        MapaRenderizado = c.Boolean(nullable: false),
                        EstacasGeradas = c.Boolean(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClasseDeProjeto", t => t.ClasseDeProjetoId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.ClasseDeProjetoId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ClasseDeProjeto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        Caracteristicas = c.String(unicode: false),
                        CritClassTecnica = c.String(unicode: false),
                        Relevo = c.Int(nullable: false),
                        VelProjeto = c.Int(nullable: false),
                        VelDirMin = c.Int(nullable: false),
                        DistMinVisbParMinD = c.Int(nullable: false),
                        DistMinVisbParMinA = c.Int(nullable: false),
                        DistMinVisbUltra = c.Int(nullable: false),
                        RaioMinSupEleMax = c.Int(nullable: false),
                        SupEleMax = c.Int(nullable: false),
                        RampaMax = c.Double(nullable: false),
                        KCxMinD = c.Int(nullable: false),
                        KCxMinA = c.Int(nullable: false),
                        KCvMinD = c.Int(nullable: false),
                        KCvMinA = c.Int(nullable: false),
                        LargFxTrans = c.Double(nullable: false),
                        LargAcoExtD = c.Double(nullable: false),
                        LargAcoExtA = c.Double(nullable: false),
                        LargAcoIntDuasFxMin = c.Double(nullable: false),
                        LargAcoIntTresFxMin = c.Double(nullable: false),
                        LargAcoIntQuatroFxMin = c.Double(nullable: false),
                        LargAcoIntDuasFxMax = c.Double(nullable: false),
                        LargAcoIntTresFxMax = c.Double(nullable: false),
                        LargAcoIntQuatroFxMax = c.Double(nullable: false),
                        GabVertD = c.Double(nullable: false),
                        GabVertA = c.Double(nullable: false),
                        AfastMinBordAcoObC = c.Double(nullable: false),
                        AfastMinBordAcoObI = c.Double(nullable: false),
                        LargCantCentDMin = c.Double(nullable: false),
                        LargCantCentNMin = c.Double(nullable: false),
                        LargCantCentAMin = c.Double(nullable: false),
                        LargCantCentDMax = c.Double(nullable: false),
                        LargCantCentNMax = c.Double(nullable: false),
                        LargCantCentAMax = c.Double(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CurvaHorizontal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        EstacaInicialId = c.Int(nullable: false),
                        EstacaFinalId = c.Int(nullable: false),
                        TrechoInicialId = c.Int(nullable: false),
                        TrechoFinalId = c.Int(nullable: false),
                        ProjetoId = c.Int(nullable: false),
                        Raio = c.Int(nullable: false),
                        AnguloCentral = c.Double(nullable: false),
                        Desenvolvimento = c.Double(nullable: false),
                        Corda = c.Int(nullable: false),
                        GrauDeCurva = c.Double(nullable: false),
                        Deflexao = c.Double(nullable: false),
                        TangenteExterior = c.Double(nullable: false),
                        EstacaPCId = c.Int(nullable: false),
                        EstacaPTId = c.Int(nullable: false),
                        SuperElevacao = c.Double(nullable: false),
                        VelDiretriz = c.Int(nullable: false),
                        Transicao = c.Boolean(nullable: false),
                        LcMinAbsoluto = c.Double(nullable: false),
                        LcMinFluenciaOtica = c.Double(nullable: false),
                        CTaxaMaximaAdmissivel = c.Double(nullable: false),
                        LcMinConforto = c.Double(nullable: false),
                        LcMaxAnguloCentral = c.Double(nullable: false),
                        LcMaxTempoPercurso = c.Double(nullable: false),
                        Lc = c.Double(nullable: false),
                        ACEspiral = c.Double(nullable: false),
                        AcComTransicao = c.Double(nullable: false),
                        Xc = c.Double(nullable: false),
                        Yc = c.Double(nullable: false),
                        Q = c.Double(nullable: false),
                        P = c.Double(nullable: false),
                        DesenvolvimentoCircular = c.Double(nullable: false),
                        EstacaTSId = c.Int(nullable: false),
                        EstacaSCId = c.Int(nullable: false),
                        EstacaCSId = c.Int(nullable: false),
                        EstacaSTId = c.Int(nullable: false),
                        Co = c.Double(nullable: false),
                        Cc = c.Double(nullable: false),
                        Io = c.Double(nullable: false),
                        Jo = c.Double(nullable: false),
                        Teta = c.Double(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estaca", t => t.EstacaCSId)
                .ForeignKey("dbo.Estaca", t => t.EstacaPCId)
                .ForeignKey("dbo.Estaca", t => t.EstacaPTId)
                .ForeignKey("dbo.Estaca", t => t.EstacaSCId)
                .ForeignKey("dbo.Estaca", t => t.EstacaSTId)
                .ForeignKey("dbo.Estaca", t => t.EstacaTSId)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .ForeignKey("dbo.Trecho", t => t.TrechoFinalId)
                .ForeignKey("dbo.Trecho", t => t.TrechoInicialId)
                .Index(t => t.TrechoInicialId)
                .Index(t => t.TrechoFinalId)
                .Index(t => t.ProjetoId)
                .Index(t => t.EstacaPCId)
                .Index(t => t.EstacaPTId)
                .Index(t => t.EstacaTSId)
                .Index(t => t.EstacaSCId)
                .Index(t => t.EstacaCSId)
                .Index(t => t.EstacaSTId);
            
            CreateTable(
                "dbo.Estaca",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.Double(nullable: false),
                        Intermediario = c.Double(nullable: false),
                        CoordenadaId = c.Int(nullable: false),
                        CotaVermelha = c.Double(nullable: false),
                        ProjetoId = c.Int(nullable: false),
                        Relatorio = c.Boolean(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordenada", t => t.CoordenadaId)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .Index(t => t.CoordenadaId)
                .Index(t => t.ProjetoId);
            
            CreateTable(
                "dbo.Trecho",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        EstacaInicialId = c.Int(nullable: false),
                        EstacaFinalId = c.Int(nullable: false),
                        PontoInicialId = c.Int(nullable: false),
                        PontoFinalId = c.Int(nullable: false),
                        Distancia = c.Double(nullable: false),
                        Inclinacao = c.Double(nullable: false),
                        Azimute = c.Double(nullable: false),
                        ProjetoId = c.Int(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estaca", t => t.EstacaFinalId)
                .ForeignKey("dbo.Estaca", t => t.EstacaInicialId)
                .ForeignKey("dbo.PontoNotavel", t => t.PontoFinalId)
                .ForeignKey("dbo.PontoNotavel", t => t.PontoInicialId)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .Index(t => t.EstacaInicialId)
                .Index(t => t.EstacaFinalId)
                .Index(t => t.PontoInicialId)
                .Index(t => t.PontoFinalId)
                .Index(t => t.ProjetoId);

            CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 256, storeType: "nvarchar"),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(unicode: false),
                        SecurityStamp = c.String(unicode: false),
                        PhoneNumber = c.String(unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(precision: 0),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ClaimType = c.String(unicode: false),
                        ClaimValue = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        ProviderKey = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.aspnetclaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CurvaVertical",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        ProjetoId = c.Int(nullable: false),
                        PontoNotavelVerticalId = c.Int(nullable: false),
                        TipoVertical = c.Int(nullable: false),
                        VelDiretriz = c.Double(nullable: false),
                        K = c.Double(nullable: false),
                        IInicial = c.Double(nullable: false),
                        IFinal = c.Double(nullable: false),
                        A = c.Double(nullable: false),
                        L = c.Double(nullable: false),
                        DistVisPara = c.Double(nullable: false),
                        RaioMinimoCurv = c.Double(nullable: false),
                        OMax = c.Double(nullable: false),
                        EstacaPCVId = c.Int(nullable: false),
                        EstacaPIVId = c.Int(nullable: false),
                        EstacaPTVId = c.Int(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estaca", t => t.EstacaPCVId)
                .ForeignKey("dbo.Estaca", t => t.EstacaPIVId)
                .ForeignKey("dbo.Estaca", t => t.EstacaPTVId)
                .ForeignKey("dbo.PontoNotavelVertical", t => t.PontoNotavelVerticalId)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .Index(t => t.ProjetoId)
                .Index(t => t.PontoNotavelVerticalId)
                .Index(t => t.EstacaPCVId)
                .Index(t => t.EstacaPIVId)
                .Index(t => t.EstacaPTVId);
            
            CreateTable(
                "dbo.PontoNotavelVertical",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        CoordenadaId = c.Int(nullable: false),
                        ProjetoId = c.Int(nullable: false),
                        EstacaId = c.Int(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Coordenada", t => t.CoordenadaId)
                .ForeignKey("dbo.Estaca", t => t.EstacaId)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .Index(t => t.CoordenadaId)
                .Index(t => t.ProjetoId)
                .Index(t => t.EstacaId);
            
            CreateTable(
                "dbo.EditProjetoViewModel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(unicode: false),
                        ClasseDeProjetoId = c.Int(nullable: false),
                        PontoInicialId = c.Int(nullable: false),
                        PontoFinalId = c.Int(nullable: false),
                        NomeResponsavel = c.String(unicode: false),
                        NumeroDePontos = c.Int(nullable: false),
                        NumeroDeInicializadoresDeCurvas = c.Int(nullable: false),
                        NumeroDeCurvas = c.Int(nullable: false),
                        NumeroDeTrechos = c.Int(nullable: false),
                        CoordenadasReais = c.String(unicode: false),
                        CoordenadasReaisBool = c.Boolean(nullable: false),
                        MapaRenderizado = c.Boolean(nullable: false),
                        EstacasGeradas = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClasseDeProjeto", t => t.ClasseDeProjetoId)
                .Index(t => t.ClasseDeProjetoId);
            
            CreateTable(
                "dbo.InicializadorDeCurva",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProjetoId = c.Int(nullable: false),
                        TrechoInicialId = c.Int(nullable: false),
                        TrechoFinalId = c.Int(nullable: false),
                        Nome = c.String(unicode: false),
                        Raio = c.Int(nullable: false),
                        Lc = c.Int(nullable: false),
                        SuperElevacao = c.Double(nullable: false),
                        VelDiretriz = c.Int(nullable: false),
                        Corda = c.Int(nullable: false),
                        CurvaInicializada = c.Boolean(nullable: false),
                        DateIncluded = c.DateTime(nullable: false, precision: 0),
                        DateAltered = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId)
                .ForeignKey("dbo.Trecho", t => t.TrechoFinalId)
                .ForeignKey("dbo.Trecho", t => t.TrechoInicialId)
                .Index(t => t.ProjetoId)
                .Index(t => t.TrechoInicialId)
                .Index(t => t.TrechoFinalId);

            CreateTable(
                    "dbo.AspNetRoles",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.InicializadorDeCurva", "TrechoInicialId", "dbo.Trecho");
            DropForeignKey("dbo.InicializadorDeCurva", "TrechoFinalId", "dbo.Trecho");
            DropForeignKey("dbo.InicializadorDeCurva", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.EditProjetoViewModel", "ClasseDeProjetoId", "dbo.ClasseDeProjeto");
            DropForeignKey("dbo.CurvaVertical", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.CurvaVertical", "PontoNotavelVerticalId", "dbo.PontoNotavelVertical");
            DropForeignKey("dbo.PontoNotavelVertical", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.PontoNotavelVertical", "EstacaId", "dbo.Estaca");
            DropForeignKey("dbo.PontoNotavelVertical", "CoordenadaId", "dbo.Coordenada");
            DropForeignKey("dbo.CurvaVertical", "EstacaPTVId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaVertical", "EstacaPIVId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaVertical", "EstacaPCVId", "dbo.Estaca");
            DropForeignKey("dbo.Azimute", "PontoNotavelId", "dbo.PontoNotavel");
            DropForeignKey("dbo.Projeto", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.PontoNotavel", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.CurvaHorizontal", "TrechoInicialId", "dbo.Trecho");
            DropForeignKey("dbo.CurvaHorizontal", "TrechoFinalId", "dbo.Trecho");
            DropForeignKey("dbo.Trecho", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.Trecho", "PontoInicialId", "dbo.PontoNotavel");
            DropForeignKey("dbo.Trecho", "PontoFinalId", "dbo.PontoNotavel");
            DropForeignKey("dbo.Trecho", "EstacaInicialId", "dbo.Estaca");
            DropForeignKey("dbo.Trecho", "EstacaFinalId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaHorizontal", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.CurvaHorizontal", "EstacaTSId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaHorizontal", "EstacaSTId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaHorizontal", "EstacaSCId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaHorizontal", "EstacaPTId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaHorizontal", "EstacaPCId", "dbo.Estaca");
            DropForeignKey("dbo.CurvaHorizontal", "EstacaCSId", "dbo.Estaca");
            DropForeignKey("dbo.Estaca", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.Estaca", "CoordenadaId", "dbo.Coordenada");
            DropForeignKey("dbo.Projeto", "ClasseDeProjetoId", "dbo.ClasseDeProjeto");
            DropForeignKey("dbo.Azimute", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.PontoNotavel", "CoordenadaId", "dbo.Coordenada");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.InicializadorDeCurva", new[] { "TrechoFinalId" });
            DropIndex("dbo.InicializadorDeCurva", new[] { "TrechoInicialId" });
            DropIndex("dbo.InicializadorDeCurva", new[] { "ProjetoId" });
            DropIndex("dbo.EditProjetoViewModel", new[] { "ClasseDeProjetoId" });
            DropIndex("dbo.PontoNotavelVertical", new[] { "EstacaId" });
            DropIndex("dbo.PontoNotavelVertical", new[] { "ProjetoId" });
            DropIndex("dbo.PontoNotavelVertical", new[] { "CoordenadaId" });
            DropIndex("dbo.CurvaVertical", new[] { "EstacaPTVId" });
            DropIndex("dbo.CurvaVertical", new[] { "EstacaPIVId" });
            DropIndex("dbo.CurvaVertical", new[] { "EstacaPCVId" });
            DropIndex("dbo.CurvaVertical", new[] { "PontoNotavelVerticalId" });
            DropIndex("dbo.CurvaVertical", new[] { "ProjetoId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Trecho", new[] { "ProjetoId" });
            DropIndex("dbo.Trecho", new[] { "PontoFinalId" });
            DropIndex("dbo.Trecho", new[] { "PontoInicialId" });
            DropIndex("dbo.Trecho", new[] { "EstacaFinalId" });
            DropIndex("dbo.Trecho", new[] { "EstacaInicialId" });
            DropIndex("dbo.Estaca", new[] { "ProjetoId" });
            DropIndex("dbo.Estaca", new[] { "CoordenadaId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "EstacaSTId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "EstacaCSId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "EstacaSCId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "EstacaTSId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "EstacaPTId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "EstacaPCId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "ProjetoId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "TrechoFinalId" });
            DropIndex("dbo.CurvaHorizontal", new[] { "TrechoInicialId" });
            DropIndex("dbo.Projeto", new[] { "UserId" });
            DropIndex("dbo.Projeto", new[] { "ClasseDeProjetoId" });
            DropIndex("dbo.PontoNotavel", new[] { "ProjetoId" });
            DropIndex("dbo.PontoNotavel", new[] { "CoordenadaId" });
            DropIndex("dbo.Azimute", new[] { "ProjetoId" });
            DropIndex("dbo.Azimute", new[] { "PontoNotavelId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.InicializadorDeCurva");
            DropTable("dbo.EditProjetoViewModel");
            DropTable("dbo.PontoNotavelVertical");
            DropTable("dbo.CurvaVertical");
            DropTable("dbo.aspnetclaims");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Trecho");
            DropTable("dbo.Estaca");
            DropTable("dbo.CurvaHorizontal");
            DropTable("dbo.ClasseDeProjeto");
            DropTable("dbo.Projeto");
            DropTable("dbo.Coordenada");
            DropTable("dbo.PontoNotavel");
            DropTable("dbo.Azimute");
        }
    }
}
