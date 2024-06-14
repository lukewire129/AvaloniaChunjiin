using System;

namespace AvaloniaChunjiin.Services;

public class KeypadService
{
    private string _LanType = "kor";
    private string LastKeyValue = null;
    private bool bFirst = true; // 누른 키가 처음 눌린것인지 아닌지 여부
    private int nChange = 0; // 눌렸을 경우에는 몇번 눌렸던것인지
    private string _InputText = null;
    private string _InputTextString = null;
    private Automata automata = new Automata();

    private readonly string[] Kor_Table = new string[11]
    {
        "l", "·", "m",
        "rzR", "sf", "exE",
        "qvQ", "tgT", "wcW",
        "da", " "
    };

    private readonly string[] Eng_Table = new string[11]
    {
        "ABC", "DEF", "GHI",
        "JKL", "MNO", "PQR",
        "STU", "VWX", "YZ",
        "", " "
    };

    public KeypadService()
    {
    }

    public void init()
    {
        bFirst = true;
        nChange = 0;
        LastKeyValue = null;
    }

    public string fc_Get_KeyCode(int nIdx)
    {
        try
        {
            switch (_LanType)
            {
                case "kor": // 한글 모드
                    return Kor_Table[nIdx];
                case "eng": // 영문 모드
                    return Eng_Table[nIdx];
            }
        }
        catch (Exception err)
        {
        }

        return "";
    }

    public string LanType
    {
        get { return _LanType; }
        set { _LanType = value; }
    }

    public void LanTypeChange()
    {
        if (_LanType == "kor")
        {
            _LanType = "eng";
        }
        else
        {
            _LanType = "kor";
        }
    }

    private string ConvertVowel(string sTmp)
    {
        try
        {
            sTmp = sTmp.Replace("··l", "u");
            //천지인 문자 처리
            sTmp = sTmp.Replace("··m", "y");
            sTmp = sTmp.Replace("m··", "b");
            sTmp = sTmp.Replace("·m", "h");
            sTmp = sTmp.Replace("m·", "n");
            sTmp = sTmp.Replace("·l", "j");
            sTmp = sTmp.Replace("l·", "k");
            sTmp = sTmp.Replace("k·", "i");
            sTmp = sTmp.Replace("n·", "b");
            sTmp = sTmp.Replace("kl", "o");
            sTmp = sTmp.Replace("jl", "p");
            sTmp = sTmp.Replace("il", "O");
            sTmp = sTmp.Replace("ul", "P");
            sTmp = sTmp.Replace("bl", "nj");
        }
        catch (Exception err)
        {
        }

        return sTmp;
    }

    private string ConvertVowelOption(string sTmp)
    {
        string sTmp_Tmp = sTmp.Substring(0, sTmp.Length - 2);
        try
        {
            sTmp = sTmp.Substring(sTmp.Length - 2, 2);
            sTmp = sTmp.Replace("u·", "j");
            //천지인 문자 처리
            sTmp = sTmp.Replace("i·", "k");
            sTmp = sTmp.Replace("y·", "h");
            sTmp = sTmp.Replace("b·", "n");
            sTmp_Tmp += sTmp;
        }
        catch (Exception err)
        {
        }

        return sTmp_Tmp;
    }

    public void InputText(string idx)
    {
        this._InputTextString += idx;
    }
    /// <summary>
    /// 입력된 키패드값을 전달
    /// </summary>
    /// <param name="idx"></param>
    /// <returns> 입력 키 타이머를 사용시 True 미사용시 False</returns>
    public bool InputText(int idx)
    {
        string sKey2 = fc_Get_KeyCode(idx);
        bFirst = false;

        if (LastKeyValue != sKey2)
        {
            bFirst = true;
            nChange = 0;
        }

        if (idx == 10)
        {
            _InputTextString += " ";
            _InputText += " ";
            return true;
        }

        LastKeyValue = sKey2;

        if (_LanType == "kor")
        {
            switch (idx)
            {
                case 0:
                case 1:
                case 2:
                    bFirst = true; //ㅣ . ㅡ 버튼시 다음글자로 넘김						
                    break;
            }

            if (idx == 0 || idx == 2)
            {
                nChange = 0;
            }

            if (idx == 1 && nChange == 2) // '·' 입력이 
            {
                Set_KorText(true);
            }
            else
            {
                Set_KorText();
            }
        }
        else
        {
            Set_EngText();
        }

        if (_LanType == "kor" && idx == 1) 
        {
            if (nChange == 2)
            {
                nChange = 1;
            }
            else
            {
                ++nChange;
            }

            return false;
        }
        else if (_LanType == "kor" && (idx == 4) || (idx == 9))
        {
            if (++nChange == 2)
                nChange = 0;
            return true;
        }

        if (++nChange == 3)
            nChange = 0;

        return true;
    }
    

    private void Set_KorText(bool bOption = false)
    {
        try
        {
            string sValue = "";
            if (LastKeyValue.Length > 1)
            {
                sValue = LastKeyValue[nChange].ToString();
            }
            else
            {
                sValue = LastKeyValue[0].ToString();
            }

            if (bFirst)
            {
                _InputText += sValue;
            }
            else
            {
                if (1 < _InputText.Length)
                    _InputText = _InputText.Substring(0, _InputText.Length - 1) + sValue;
                else
                    _InputText = sValue;
            }

            if (bOption)
            {
                _InputText = ConvertVowelOption(_InputText);
            }
            else
            {
                _InputText = ConvertVowel(_InputText);
            }

            _InputTextString = automata.Eng2Kor(_InputText);
        }
        catch (Exception err)
        {
        }
    }

    private void Set_EngText()
    {
        try
        {
            string sValue = LastKeyValue[nChange].ToString();

            if (bFirst)
            {
                _InputText += sValue;
            }
            else
            {
                if (1 < _InputText.Length)
                    _InputText = _InputText.Substring(0, _InputText.Length - 1) + sValue;
                else
                    _InputText = sValue;
            }

            _InputTextString = _InputText;
        }
        catch (Exception err)
        {
        }
    }

    public string InputTextString
    {
        get
        {
            //automata.Check(ref _InputTextString);
            return _InputTextString;
        }
    }

    public void Cor()
    {
        if (String.IsNullOrEmpty(_InputTextString))
        {
            _InputText = null;
            return;
        }

        int leftlen = _InputTextString.Length;

        if (leftlen > 0)
            _InputTextString = _InputTextString.Substring(0, leftlen - 1);
        if (String.IsNullOrEmpty(_InputTextString))
        {
            _InputText = null;
            return;
        }

        _InputText = automata.Kor2Eng(_InputTextString);
    }

    public void Clear()
    {
        _InputTextString = null;
        _InputText = null;
        init();
    }

    public void Spacebar()
    {
        _InputText += " ";
    }
}