using Data;
using System;


namespace Biz.Data
{
    [TableName("SITREM", "AFECTADO")]
    public class Afectado : BizObject
    {
        Decimal? _cafectado;
        Decimal? _cincidente;
        String _nombre;
        String _apellidos;
        String _edad;
        String _amsd;
        String _sexo;
        String _dirTramo;
        String _dirNro;
        String _dni;
        String _telefono;
        String _diagmc1;
        String _diagmc2;
        String _dinsitu1;
        String _dinsitu2;
        String _dinsitu3;
        String _dhospit1;
        String _dhospit2;
        String _dhospit3;
        String _causa1;
        String _causa2;
        String _causa3;
        String _maniobras;
        String _valoracion;
        String _destino;
        String _obsmedc;
        String _cip;
        String _estado;
        String _finalizacion;
        String _dirComp;
        String _consulta;
        String _condicion;
        String _matricula;
        String _color;
        String _tipovehiculo;
        String _estadopais;
        String _marcamodelo;
        String _otros;
        Decimal? _locAsist;
        DateTime? _datosadministrativos;
        String _coheCentral;
        String _coheInsitu;
        String _corrCentral;
        String _corrInsitu;
        String _avaluacUsuari;
        String _ctPci;
        String _ctCom;
        String _ctMun;
        String _ctSm1;
        String _ctSm2;
        Decimal? _calertante;
        DateTime? _fecCrea;
        String _resolucion;
        Decimal? _cusuario;
        String _grupRisc;
        Decimal? _codiEspecial;
        String _codespInfo;
        String _codespUsrdata1;
        String _codespUsrdata2;
        Decimal? _idSr;
        Decimal? _estadoSr;
        String _hc3User;
        DateTime? _hc3Data;
        String _dirTxt;
        Decimal? _dirX;
        Decimal? _dirY;
        String _esc;
        String _pis;
        String _porta;
        String _dirSigla;
        String _dirTipo;
        Decimal? _organizationId;
        Decimal? _idInsurance;
        String _insuranceNum;
        Decimal? _cuserapp;
        String _origenCip;
        String _origenGrisc;
        Decimal? _causa1Versio;
        Decimal? _causa2Versio;
        Decimal? _causa3Versio;
        Decimal? _dhospit1Versio;
        Decimal? _dhospit2Versio;
        Decimal? _dhospit3Versio;
        Decimal? _diagmc1Versio;
        Decimal? _diagmc2Versio;
        Decimal? _dinsitu1Versio;
        Decimal? _dinsitu2Versio;
        Decimal? _dinsitu3Versio;
        Decimal? _coberturaId;

        public Afectado() { }

        [Column("CAFECTADO")]
        public Decimal? Cafectado
        {
            get { return _cafectado; }
            set { SetPropertyValue("Cafectado", ref _cafectado, value); }
        }

        [Column("CINCIDENTE")]
        public Decimal? Cincidente
        {
            get { return _cincidente; }
            set { SetPropertyValue("Cincidente", ref _cincidente, value); }
        }

        [Column("NOMBRE")]
        public String Nombre
        {
            get { return _nombre; }
            set { SetPropertyValue("Nombre", ref _nombre, value); }
        }

        [Column("APELLIDOS")]
        public String Apellidos
        {
            get { return _apellidos; }
            set { SetPropertyValue("Apellidos", ref _apellidos, value); }
        }

        [Column("EDAD")]
        public String Edad
        {
            get { return _edad; }
            set { SetPropertyValue("Edad", ref _edad, value); }
        }

        [Column("AMSD")]
        public String Amsd
        {
            get { return _amsd; }
            set { SetPropertyValue("Amsd", ref _amsd, value); }
        }

        [Column("SEXO")]
        public String Sexo
        {
            get { return _sexo; }
            set { SetPropertyValue("Sexo", ref _sexo, value); }
        }

        [Column("DIR_TRAMO")]
        public String DirTramo
        {
            get { return _dirTramo; }
            set { SetPropertyValue("DirTramo", ref _dirTramo, value); }
        }

        [Column("DIR_NRO")]
        public String DirNro
        {
            get { return _dirNro; }
            set { SetPropertyValue("DirNro", ref _dirNro, value); }
        }

        [Column("DNI")]
        public String Dni
        {
            get { return _dni; }
            set { SetPropertyValue("Dni", ref _dni, value); }
        }

        [Column("TELEFONO")]
        public String Telefono
        {
            get { return _telefono; }
            set { SetPropertyValue("Telefono", ref _telefono, value); }
        }

        [Column("DIAGMC1")]
        public String Diagmc1
        {
            get { return _diagmc1; }
            set { SetPropertyValue("Diagmc1", ref _diagmc1, value); }
        }

        [Column("DIAGMC2")]
        public String Diagmc2
        {
            get { return _diagmc2; }
            set { SetPropertyValue("Diagmc2", ref _diagmc2, value); }
        }

        [Column("DINSITU1")]
        public String Dinsitu1
        {
            get { return _dinsitu1; }
            set { SetPropertyValue("Dinsitu1", ref _dinsitu1, value); }
        }

        [Column("DINSITU2")]
        public String Dinsitu2
        {
            get { return _dinsitu2; }
            set { SetPropertyValue("Dinsitu2", ref _dinsitu2, value); }
        }

        [Column("DINSITU3")]
        public String Dinsitu3
        {
            get { return _dinsitu3; }
            set { SetPropertyValue("Dinsitu3", ref _dinsitu3, value); }
        }

        [Column("DHOSPIT1")]
        public String Dhospit1
        {
            get { return _dhospit1; }
            set { SetPropertyValue("Dhospit1", ref _dhospit1, value); }
        }

        [Column("DHOSPIT2")]
        public String Dhospit2
        {
            get { return _dhospit2; }
            set { SetPropertyValue("Dhospit2", ref _dhospit2, value); }
        }

        [Column("DHOSPIT3")]
        public String Dhospit3
        {
            get { return _dhospit3; }
            set { SetPropertyValue("Dhospit3", ref _dhospit3, value); }
        }

        [Column("CAUSA1")]
        public String Causa1
        {
            get { return _causa1; }
            set { SetPropertyValue("Causa1", ref _causa1, value); }
        }

        [Column("CAUSA2")]
        public String Causa2
        {
            get { return _causa2; }
            set { SetPropertyValue("Causa2", ref _causa2, value); }
        }

        [Column("CAUSA3")]
        public String Causa3
        {
            get { return _causa3; }
            set { SetPropertyValue("Causa3", ref _causa3, value); }
        }

        [Column("MANIOBRAS")]
        public String Maniobras
        {
            get { return _maniobras; }
            set { SetPropertyValue("Maniobras", ref _maniobras, value); }
        }

        [Column("VALORACION")]
        public String Valoracion
        {
            get { return _valoracion; }
            set { SetPropertyValue("Valoracion", ref _valoracion, value); }
        }

        [Column("DESTINO")]
        public String Destino
        {
            get { return _destino; }
            set { SetPropertyValue("Destino", ref _destino, value); }
        }

        [Column("OBSMEDC")]
        public String Obsmedc
        {
            get { return _obsmedc; }
            set { SetPropertyValue("Obsmedc", ref _obsmedc, value); }
        }

        [Column("CIP")]
        public String Cip
        {
            get { return _cip; }
            set { SetPropertyValue("Cip", ref _cip, value); }
        }

        [Column("ESTADO")]
        public String Estado
        {
            get { return _estado; }
            set { SetPropertyValue("Estado", ref _estado, value); }
        }

        [Column("FINALIZACION")]
        public String Finalizacion
        {
            get { return _finalizacion; }
            set { SetPropertyValue("Finalizacion", ref _finalizacion, value); }
        }

        [Column("DIR_COMP")]
        public String DirComp
        {
            get { return _dirComp; }
            set { SetPropertyValue("DirComp", ref _dirComp, value); }
        }

        [Column("CONSULTA")]
        public String Consulta
        {
            get { return _consulta; }
            set { SetPropertyValue("Consulta", ref _consulta, value); }
        }

        [Column("CONDICION")]
        public String Condicion
        {
            get { return _condicion; }
            set { SetPropertyValue("Condicion", ref _condicion, value); }
        }

        [Column("MATRICULA")]
        public String Matricula
        {
            get { return _matricula; }
            set { SetPropertyValue("Matricula", ref _matricula, value); }
        }

        [Column("COLOR")]
        public String Color
        {
            get { return _color; }
            set { SetPropertyValue("Color", ref _color, value); }
        }

        [Column("TIPOVEHICULO")]
        public String Tipovehiculo
        {
            get { return _tipovehiculo; }
            set { SetPropertyValue("Tipovehiculo", ref _tipovehiculo, value); }
        }

        [Column("ESTADOPAIS")]
        public String Estadopais
        {
            get { return _estadopais; }
            set { SetPropertyValue("Estadopais", ref _estadopais, value); }
        }

        [Column("MARCAMODELO")]
        public String Marcamodelo
        {
            get { return _marcamodelo; }
            set { SetPropertyValue("Marcamodelo", ref _marcamodelo, value); }
        }

        [Column("OTROS")]
        public String Otros
        {
            get { return _otros; }
            set { SetPropertyValue("Otros", ref _otros, value); }
        }

        [Column("LOC_ASIST")]
        public Decimal? LocAsist
        {
            get { return _locAsist; }
            set { SetPropertyValue("LocAsist", ref _locAsist, value); }
        }

        [Column("DATOSADMINISTRATIVOS")]
        public DateTime? Datosadministrativos
        {
            get { return _datosadministrativos; }
            set { SetPropertyValue("Datosadministrativos", ref _datosadministrativos, value); }
        }

        [Column("COHE_CENTRAL")]
        public String CoheCentral
        {
            get { return _coheCentral; }
            set { SetPropertyValue("CoheCentral", ref _coheCentral, value); }
        }

        [Column("COHE_INSITU")]
        public String CoheInsitu
        {
            get { return _coheInsitu; }
            set { SetPropertyValue("CoheInsitu", ref _coheInsitu, value); }
        }

        [Column("CORR_CENTRAL")]
        public String CorrCentral
        {
            get { return _corrCentral; }
            set { SetPropertyValue("CorrCentral", ref _corrCentral, value); }
        }

        [Column("CORR_INSITU")]
        public String CorrInsitu
        {
            get { return _corrInsitu; }
            set { SetPropertyValue("CorrInsitu", ref _corrInsitu, value); }
        }

        [Column("AVALUAC_USUARI")]
        public String AvaluacUsuari
        {
            get { return _avaluacUsuari; }
            set { SetPropertyValue("AvaluacUsuari", ref _avaluacUsuari, value); }
        }

        [Column("CT_PCI")]
        public String CtPci
        {
            get { return _ctPci; }
            set { SetPropertyValue("CtPci", ref _ctPci, value); }
        }

        [Column("CT_COM")]
        public String CtCom
        {
            get { return _ctCom; }
            set { SetPropertyValue("CtCom", ref _ctCom, value); }
        }

        [Column("CT_MUN")]
        public String CtMun
        {
            get { return _ctMun; }
            set { SetPropertyValue("CtMun", ref _ctMun, value); }
        }

        [Column("CT_SM1")]
        public String CtSm1
        {
            get { return _ctSm1; }
            set { SetPropertyValue("CtSm1", ref _ctSm1, value); }
        }

        [Column("CT_SM2")]
        public String CtSm2
        {
            get { return _ctSm2; }
            set { SetPropertyValue("CtSm2", ref _ctSm2, value); }
        }

        [Column("CALERTANTE")]
        public Decimal? Calertante
        {
            get { return _calertante; }
            set { SetPropertyValue("Calertante", ref _calertante, value); }
        }

        [Column("FEC_CREA")]
        public DateTime? FecCrea
        {
            get { return _fecCrea; }
            set { SetPropertyValue("FecCrea", ref _fecCrea, value); }
        }

        [Column("RESOLUCION")]
        public String Resolucion
        {
            get { return _resolucion; }
            set { SetPropertyValue("Resolucion", ref _resolucion, value); }
        }

        [Column("CUSUARIO")]
        public Decimal? Cusuario
        {
            get { return _cusuario; }
            set { SetPropertyValue("Cusuario", ref _cusuario, value); }
        }

        [Column("GRUP_RISC")]
        public String GrupRisc
        {
            get { return _grupRisc; }
            set { SetPropertyValue("GrupRisc", ref _grupRisc, value); }
        }

        [Column("CODI_ESPECIAL")]
        public Decimal? CodiEspecial
        {
            get { return _codiEspecial; }
            set { SetPropertyValue("CodiEspecial", ref _codiEspecial, value); }
        }

        [Column("CODESP_INFO")]
        public String CodespInfo
        {
            get { return _codespInfo; }
            set { SetPropertyValue("CodespInfo", ref _codespInfo, value); }
        }

        [Column("CODESP_USRDATA1")]
        public String CodespUsrdata1
        {
            get { return _codespUsrdata1; }
            set { SetPropertyValue("CodespUsrdata1", ref _codespUsrdata1, value); }
        }

        [Column("CODESP_USRDATA2")]
        public String CodespUsrdata2
        {
            get { return _codespUsrdata2; }
            set { SetPropertyValue("CodespUsrdata2", ref _codespUsrdata2, value); }
        }

        [Column("ID_SR")]
        public Decimal? IdSr
        {
            get { return _idSr; }
            set { SetPropertyValue("IdSr", ref _idSr, value); }
        }

        [Column("ESTADO_SR")]
        public Decimal? EstadoSr
        {
            get { return _estadoSr; }
            set { SetPropertyValue("EstadoSr", ref _estadoSr, value); }
        }

        [Column("HC3_USER")]
        public String Hc3User
        {
            get { return _hc3User; }
            set { SetPropertyValue("Hc3User", ref _hc3User, value); }
        }

        [Column("HC3_DATA")]
        public DateTime? Hc3Data
        {
            get { return _hc3Data; }
            set { SetPropertyValue("Hc3Data", ref _hc3Data, value); }
        }

        [Column("DIR_TXT")]
        public String DirTxt
        {
            get { return _dirTxt; }
            set { SetPropertyValue("DirTxt", ref _dirTxt, value); }
        }

        [Column("DIR_X")]
        public Decimal? DirX
        {
            get { return _dirX; }
            set { SetPropertyValue("DirX", ref _dirX, value); }
        }

        [Column("DIR_Y")]
        public Decimal? DirY
        {
            get { return _dirY; }
            set { SetPropertyValue("DirY", ref _dirY, value); }
        }

        [Column("ESC")]
        public String Esc
        {
            get { return _esc; }
            set { SetPropertyValue("Esc", ref _esc, value); }
        }

        [Column("PIS")]
        public String Pis
        {
            get { return _pis; }
            set { SetPropertyValue("Pis", ref _pis, value); }
        }

        [Column("PORTA")]
        public String Porta
        {
            get { return _porta; }
            set { SetPropertyValue("Porta", ref _porta, value); }
        }

        [Column("DIR_SIGLA")]
        public String DirSigla
        {
            get { return _dirSigla; }
            set { SetPropertyValue("DirSigla", ref _dirSigla, value); }
        }

        [Column("DIR_TIPO")]
        public String DirTipo
        {
            get { return _dirTipo; }
            set { SetPropertyValue("DirTipo", ref _dirTipo, value); }
        }

        [Column("ORGANIZATION_ID")]
        public Decimal? OrganizationId
        {
            get { return _organizationId; }
            set { SetPropertyValue("OrganizationId", ref _organizationId, value); }
        }

        [Column("ID_INSURANCE")]
        public Decimal? IdInsurance
        {
            get { return _idInsurance; }
            set { SetPropertyValue("IdInsurance", ref _idInsurance, value); }
        }

        [Column("INSURANCE_NUM")]
        public String InsuranceNum
        {
            get { return _insuranceNum; }
            set { SetPropertyValue("InsuranceNum", ref _insuranceNum, value); }
        }

        [Column("CUSERAPP")]
        public Decimal? Cuserapp
        {
            get { return _cuserapp; }
            set { SetPropertyValue("Cuserapp", ref _cuserapp, value); }
        }

        [Column("ORIGEN_CIP")]
        public String OrigenCip
        {
            get { return _origenCip; }
            set { SetPropertyValue("OrigenCip", ref _origenCip, value); }
        }

        [Column("ORIGEN_GRISC")]
        public String OrigenGrisc
        {
            get { return _origenGrisc; }
            set { SetPropertyValue("OrigenGrisc", ref _origenGrisc, value); }
        }

        [Column("CAUSA1_VERSIO")]
        public Decimal? Causa1Versio
        {
            get { return _causa1Versio; }
            set { SetPropertyValue("Causa1Versio", ref _causa1Versio, value); }
        }

        [Column("CAUSA2_VERSIO")]
        public Decimal? Causa2Versio
        {
            get { return _causa2Versio; }
            set { SetPropertyValue("Causa2Versio", ref _causa2Versio, value); }
        }

        [Column("CAUSA3_VERSIO")]
        public Decimal? Causa3Versio
        {
            get { return _causa3Versio; }
            set { SetPropertyValue("Causa3Versio", ref _causa3Versio, value); }
        }

        [Column("DHOSPIT1_VERSIO")]
        public Decimal? Dhospit1Versio
        {
            get { return _dhospit1Versio; }
            set { SetPropertyValue("Dhospit1Versio", ref _dhospit1Versio, value); }
        }

        [Column("DHOSPIT2_VERSIO")]
        public Decimal? Dhospit2Versio
        {
            get { return _dhospit2Versio; }
            set { SetPropertyValue("Dhospit2Versio", ref _dhospit2Versio, value); }
        }

        [Column("DHOSPIT3_VERSIO")]
        public Decimal? Dhospit3Versio
        {
            get { return _dhospit3Versio; }
            set { SetPropertyValue("Dhospit3Versio", ref _dhospit3Versio, value); }
        }

        [Column("DIAGMC1_VERSIO")]
        public Decimal? Diagmc1Versio
        {
            get { return _diagmc1Versio; }
            set { SetPropertyValue("Diagmc1Versio", ref _diagmc1Versio, value); }
        }

        [Column("DIAGMC2_VERSIO")]
        public Decimal? Diagmc2Versio
        {
            get { return _diagmc2Versio; }
            set { SetPropertyValue("Diagmc2Versio", ref _diagmc2Versio, value); }
        }

        [Column("DINSITU1_VERSIO")]
        public Decimal? Dinsitu1Versio
        {
            get { return _dinsitu1Versio; }
            set { SetPropertyValue("Dinsitu1Versio", ref _dinsitu1Versio, value); }
        }

        [Column("DINSITU2_VERSIO")]
        public Decimal? Dinsitu2Versio
        {
            get { return _dinsitu2Versio; }
            set { SetPropertyValue("Dinsitu2Versio", ref _dinsitu2Versio, value); }
        }

        [Column("DINSITU3_VERSIO")]
        public Decimal? Dinsitu3Versio
        {
            get { return _dinsitu3Versio; }
            set { SetPropertyValue("Dinsitu3Versio", ref _dinsitu3Versio, value); }
        }

        [Column("COBERTURA_ID")]
        public Decimal? CoberturaId
        {
            get { return _coberturaId; }
            set { SetPropertyValue("CoberturaId", ref _coberturaId, value); }
        }

        public static Afectado Read(System.Data.IDataReader rdr)
        {
            Afectado item = new Afectado();
            item.Cafectado = ParseValue<Decimal?>(rdr["CAFECTADO"]);
            item.Cincidente = ParseValue<Decimal?>(rdr["CINCIDENTE"]);
            item.Nombre = ParseValue<string>(rdr["NOMBRE"]);
            item.Apellidos = ParseValue<string>(rdr["APELLIDOS"]);
            item.Edad = ParseValue<string>(rdr["EDAD"]);
            item.Amsd = ParseValue<string>(rdr["AMSD"]);
            item.Sexo = ParseValue<string>(rdr["SEXO"]);
            item.Telefono = ParseValue<string>(rdr["TELEFONO"]);
            item.DirTramo = ParseValue<string>(rdr["DIR_TRAMO"]);
            item.DirNro = ParseValue<string>(rdr["DIR_NRO"]);
            item.Dni = ParseValue<string>(rdr["DNI"]);
            item.Telefono = ParseValue<string>(rdr["TELEFONO"]);
            item.Diagmc1 = ParseValue<string>(rdr["DIAGMC1"]);
            item.Diagmc2 = ParseValue<string>(rdr["DIAGMC2"]);
            item.Dinsitu1 = ParseValue<string>(rdr["DINSITU1"]);
            item.Dinsitu2 = ParseValue<string>(rdr["DINSITU2"]);
            item.Dinsitu3 = ParseValue<string>(rdr["DINSITU3"]);
            item.Dhospit1 = ParseValue<string>(rdr["DHOSPIT1"]);
            item.Dhospit2 = ParseValue<string>(rdr["DHOSPIT2"]);
            item.Dhospit3 = ParseValue<string>(rdr["DHOSPIT3"]);
            item.Causa1 = ParseValue<string>(rdr["CAUSA1"]);
            item.Causa2 = ParseValue<string>(rdr["CAUSA2"]);
            item.Causa3 = ParseValue<string>(rdr["CAUSA3"]);
            item.Maniobras= ParseValue<string>(rdr["MANIOBRAS"]);
            item.Valoracion = ParseValue<string>(rdr["VALORACION"]);
            item.Destino= ParseValue<string>(rdr["DESTINO"]);
            item.Obsmedc = ParseValue<string>(rdr["OBSERMEDC"]);
            item.Cip = ParseValue<string>(rdr["CIP"]);
            item.Estado= ParseValue<string>(rdr["ESTADO"]);
            item.Finalizacion = ParseValue<string>(rdr["FINALIZACION"]);
            item.DirComp = ParseValue<string>(rdr["DIRCOMP"]);
            item.Consulta= ParseValue<string>(rdr["CONSULTA"]);
            item.Condicion = ParseValue<string>(rdr["CONDICION"]);
            item.Matricula = ParseValue<string>(rdr["MATRICULA"]);
            item.Color = ParseValue<string>(rdr["COLOR"]);
            item.Tipovehiculo = ParseValue<string>(rdr["TIPOVEHICULO"]);
            item.Estadopais = ParseValue<string>(rdr["ESTADOPAIS"]);
            item.Marcamodelo = ParseValue<string>(rdr["MARCAMODELO"]);
            item.Otros = ParseValue<string>(rdr["OTROS"]);
            item.LocAsist = ParseValue<Decimal?>(rdr["LOC_ASIST"]);
            item.Datosadministrativos = ParseValue<DateTime?>(rdr["DATOSADMINISTRATIVOS"]);
            item.CoheCentral= ParseValue<string>(rdr["COHE_CENTRAL"]);
            item.CoheInsitu = ParseValue<string>(rdr["COHE_INSITU"]);
            item.CorrCentral = ParseValue<string>(rdr["CORR_CENTRAL"]);
            item.CorrInsitu = ParseValue<string>(rdr["CORR_INSITU"]);
            item.AvaluacUsuari = ParseValue<string>(rdr["AVALUAC_USUARI"]);
            item.CtPci = ParseValue<string>(rdr["CT_PCI"]);
            item.CtCom = ParseValue<string>(rdr["CT_COM"]);
            item.CtMun = ParseValue<string>(rdr["CT_MUN"]);
            item.CtSm1 = ParseValue<string>(rdr["CT_SM1"]);
            item.CtSm2 = ParseValue<string>(rdr["CT_SM2"]);
            item.Calertante = ParseValue<Decimal?>(rdr["CALERTANTE"]);
            item.FecCrea = ParseValue<DateTime?>(rdr["FEC_CREA"]);
            item.Resolucion = ParseValue<string>(rdr["RESOLUCION"]);
            item.Cusuario = ParseValue<Decimal?>(rdr["CUSUARIO"]);
            item.GrupRisc = ParseValue<string>(rdr["GRUP_RISC"]);
            item.CodiEspecial = ParseValue<Decimal?>(rdr["CODI_ESPECIAL"]);
            item.CodespInfo = ParseValue<string>(rdr["CODESP_INFO"]);
            item.CodespUsrdata1 = ParseValue<string>(rdr["CODESP_USRDATA1"]);
            item.CodespUsrdata2 = ParseValue<string>(rdr["CODESP_USRDATA2"]);
            item.IdSr = ParseValue<Decimal?>(rdr["ID_SR"]);
            item.EstadoSr = ParseValue<Decimal?>(rdr["ESTADO_SR"]);
            item.Hc3User = ParseValue<string>(rdr["HC3_USER"]);
            item.Hc3Data = ParseValue<DateTime?>(rdr["HC3_DATA"]);
            item.DirTxt = ParseValue<string>(rdr["DIR_TXT"]);
            item.DirX = ParseValue<Decimal?>(rdr["DIR_X"]);
            item.DirY = ParseValue<Decimal?>(rdr["DIR_Y"]);
            item.Esc = ParseValue<string>(rdr["ESC"]);
            item.Pis = ParseValue<string>(rdr["PIS"]);
            item.Porta = ParseValue<string>(rdr["PORTA"]);
            item.DirSigla = ParseValue<string>(rdr["DIR_SIGLA"]);
            item.DirTipo = ParseValue<string>(rdr["DIR_TIPO"]);
            item.OrganizationId= ParseValue<Decimal?>(rdr["ORGANIZARION_ID"]);
            item.IdInsurance = ParseValue<Decimal?>(rdr["ID_INSURANCE"]);
            item.InsuranceNum = ParseValue<string>(rdr["INSURANCE_NUM"]);
            item.Cuserapp = ParseValue<decimal?>(rdr["CUSERAPP"]);
            item.OrigenCip = ParseValue<string>(rdr["ORIGEN_CIP"]);
            item.OrigenGrisc = ParseValue<string>(rdr["ORIGEN_GRISC"]);
            item.Causa1Versio = ParseValue<Decimal?>(rdr["CAUSA1_VERSIO"]);
            item.Causa2Versio = ParseValue<Decimal?>(rdr["CAUSA2_VERSIO"]);
            item.Causa3Versio = ParseValue<Decimal?>(rdr["CAUSA3_VERSIO"]);
            item.Dhospit1Versio = ParseValue<Decimal?>(rdr["DHOSPIT1_VERSIO"]);
            item.Dhospit2Versio = ParseValue<Decimal?>(rdr["DHOSPIT2_VERSIO"]);
            item.Dhospit3Versio = ParseValue<Decimal?>(rdr["DHOSPIT3_VERSIO"]);
            item.Diagmc1Versio = ParseValue<Decimal?>(rdr["DIAGMC1_VERSIO"]);
            item.Diagmc2Versio = ParseValue<Decimal?>(rdr["DIAGMC2_VERSIO"]);
            item.Dinsitu1Versio = ParseValue<Decimal?>(rdr["DINSITU1_VERSIO"]);
            item.Dinsitu2Versio = ParseValue<Decimal?>(rdr["DINSITU2_VERSIO"]);
            item.Dinsitu3Versio = ParseValue<Decimal?>(rdr["DINSITU3_VERSIO"]);
            item.CoberturaId = ParseValue<Decimal?>(rdr["COBERTURA_ID"]);
            return item;
        }

        private static T ParseValue<T>(object value)
        {
            if (value == DBNull.Value) return default(T);
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}

