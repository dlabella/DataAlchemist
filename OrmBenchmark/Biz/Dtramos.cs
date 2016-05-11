using Data;
using System;

namespace Biz.Data
{
    [TableName("SITREM", "DTRAMOS")]
    public class Dtramos : BizObject
    {
        String _coddtramo;
        String _parimp;
        Decimal? _pnum;
        Decimal? _unum;
        String _sigla;
        String _calle;
        Decimal? _codtae;
        Decimal? _codtal;
        String _codzbs;
        Decimal? _codzs;
        Decimal? _codzr;
        Decimal? _codzrs;
        Decimal? _codzps;
        Decimal? _codzpg;
        Decimal? _codzh;
        String _tipo1;
        String _tipo2;
        String _cpostal;
        Decimal? _xsup;
        Decimal? _ysup;
        Decimal? _xinf;
        Decimal? _yinf;
        String _origen;
        String _oldId;
        String _calleOld;
        String _ctPci;
        String _ctCom;
        String _ctMun;
        String _ctSm1;
        String _ctSm2;
        String _calleOldUnic;
        String _calleAcentuada;
        String _codzbsOld;
        String _codzbsNew;
        String _visible;
        String _control;

        public Dtramos() { }

        [Column("CODDTRAMO")]
        public String Coddtramo
        {
            get { return _coddtramo; }
            set { SetPropertyValue("Coddtramo", ref _coddtramo, value); }
        }

        [Column("PARIMP")]
        public String Parimp
        {
            get { return _parimp; }
            set { SetPropertyValue("Parimp", ref _parimp, value); }
        }

        [Column("PNUM")]
        public Decimal? Pnum
        {
            get { return _pnum; }
            set { SetPropertyValue("Pnum", ref _pnum, value); }
        }

        [Column("UNUM")]
        public Decimal? Unum
        {
            get { return _unum; }
            set { SetPropertyValue("Unum", ref _unum, value); }
        }

        [Column("SIGLA")]
        public String Sigla
        {
            get { return _sigla; }
            set { SetPropertyValue("Sigla", ref _sigla, value); }
        }

        [Column("CALLE")]
        public String Calle
        {
            get { return _calle; }
            set { SetPropertyValue("Calle", ref _calle, value); }
        }

        [Column("CODTAE")]
        public Decimal? Codtae
        {
            get { return _codtae; }
            set { SetPropertyValue("Codtae", ref _codtae, value); }
        }

        [Column("CODTAL")]
        public Decimal? Codtal
        {
            get { return _codtal; }
            set { SetPropertyValue("Codtal", ref _codtal, value); }
        }

        [Column("CODZBS")]
        public String Codzbs
        {
            get { return _codzbs; }
            set { SetPropertyValue("Codzbs", ref _codzbs, value); }
        }

        [Column("CODZS")]
        public Decimal? Codzs
        {
            get { return _codzs; }
            set { SetPropertyValue("Codzs", ref _codzs, value); }
        }

        [Column("CODZR")]
        public Decimal? Codzr
        {
            get { return _codzr; }
            set { SetPropertyValue("Codzr", ref _codzr, value); }
        }

        [Column("CODZRS")]
        public Decimal? Codzrs
        {
            get { return _codzrs; }
            set { SetPropertyValue("Codzrs", ref _codzrs, value); }
        }

        [Column("CODZPS")]
        public Decimal? Codzps
        {
            get { return _codzps; }
            set { SetPropertyValue("Codzps", ref _codzps, value); }
        }

        [Column("CODZPG")]
        public Decimal? Codzpg
        {
            get { return _codzpg; }
            set { SetPropertyValue("Codzpg", ref _codzpg, value); }
        }

        [Column("CODZH")]
        public Decimal? Codzh
        {
            get { return _codzh; }
            set { SetPropertyValue("Codzh", ref _codzh, value); }
        }

        [Column("TIPO1")]
        public String Tipo1
        {
            get { return _tipo1; }
            set { SetPropertyValue("Tipo1", ref _tipo1, value); }
        }

        [Column("TIPO2")]
        public String Tipo2
        {
            get { return _tipo2; }
            set { SetPropertyValue("Tipo2", ref _tipo2, value); }
        }

        [Column("CPOSTAL")]
        public String Cpostal
        {
            get { return _cpostal; }
            set { SetPropertyValue("Cpostal", ref _cpostal, value); }
        }

        [Column("XSUP")]
        public Decimal? Xsup
        {
            get { return _xsup; }
            set { SetPropertyValue("Xsup", ref _xsup, value); }
        }

        [Column("YSUP")]
        public Decimal? Ysup
        {
            get { return _ysup; }
            set { SetPropertyValue("Ysup", ref _ysup, value); }
        }

        [Column("XINF")]
        public Decimal? Xinf
        {
            get { return _xinf; }
            set { SetPropertyValue("Xinf", ref _xinf, value); }
        }

        [Column("YINF")]
        public Decimal? Yinf
        {
            get { return _yinf; }
            set { SetPropertyValue("Yinf", ref _yinf, value); }
        }

        [Column("ORIGEN")]
        public String Origen
        {
            get { return _origen; }
            set { SetPropertyValue("Origen", ref _origen, value); }
        }

        [Column("OLD_ID")]
        public String Old_Id
        {
            get { return _oldId; }
            set { SetPropertyValue("OldId", ref _oldId, value); }
        }

        [Column("CALLE_OLD")]
        public String Calle_Old
        {
            get { return _calleOld; }
            set { SetPropertyValue("CalleOld", ref _calleOld, value); }
        }

        [Column("CT_PCI")]
        public String Ct_Pci
        {
            get { return _ctPci; }
            set { SetPropertyValue("CtPci", ref _ctPci, value); }
        }

        [Column("CT_COM")]
        public String Ct_Com
        {
            get { return _ctCom; }
            set { SetPropertyValue("CtCom", ref _ctCom, value); }
        }

        [Column("CT_MUN")]
        public String Ct_Mun
        {
            get { return _ctMun; }
            set { SetPropertyValue("CtMun", ref _ctMun, value); }
        }

        [Column("CT_SM1")]
        public String Ct_Sm1
        {
            get { return _ctSm1; }
            set { SetPropertyValue("CtSm1", ref _ctSm1, value); }
        }

        [Column("CT_SM2")]
        public String Ct_Sm2
        {
            get { return _ctSm2; }
            set { SetPropertyValue("CtSm2", ref _ctSm2, value); }
        }

        [Column("CALLE_OLD_UNIC")]
        public String Calle_Old_Unic
        {
            get { return _calleOldUnic; }
            set { SetPropertyValue("CalleOldUnic", ref _calleOldUnic, value); }
        }

        [Column("CALLE_ACENTUADA")]
        public String Calle_Acentuada
        {
            get { return _calleAcentuada; }
            set { SetPropertyValue("CalleAcentuada", ref _calleAcentuada, value); }
        }

        [Column("CODZBS_OLD")]
        public String Codzbs_Old
        {
            get { return _codzbsOld; }
            set { SetPropertyValue("CodzbsOld", ref _codzbsOld, value); }
        }

        [Column("CODZBS_NEW")]
        public String Codzbs_New
        {
            get { return _codzbsNew; }
            set { SetPropertyValue("CodzbsNew", ref _codzbsNew, value); }
        }

        [Column("VISIBLE")]
        public String Visible
        {
            get { return _visible; }
            set { SetPropertyValue("Visible", ref _visible, value); }
        }

        [Column("CONTROL")]
        public String Control
        {
            get { return _control; }
            set { SetPropertyValue("Control", ref _control, value); }
        }

    }
}