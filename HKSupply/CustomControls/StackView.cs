using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;

namespace CustomControls
{
    /// <summary>
    /// Custom control, botonera de acciones para los formulario
    /// </summary>
    /// <remarks>
    /// Acciones:
    /// - Editar
    /// - Nuevo
    /// - Guardar
    /// - Cancelar
    /// 
    /// En función de los permisos que tenga el usuario en cada pantalla mostrará las acciones correspondientess
    /// </remarks>
    public partial class StackView : UserControl
    {
        #region enums
        public enum ToolbarStates
        {
            OnlyRead,
            OnlyEdit,
            OnlyEditNew,
            Edit,
            New
        }
        #endregion

        #region Constants
        private static Color ETNIA_RED = Color.FromArgb(221, 34, 29);
        #endregion

        #region Public events
        public event EventHandler EditButtonClick;
        public event EventHandler NewButtonClick;
        public event EventHandler SaveButtonClick;
        public event EventHandler CancelButtonClick;
        #endregion

        #region Private members

        private readonly bool _read;
        private readonly bool _new;
        private readonly bool _modify;
        private ToolbarStates _currentState;

        //private static Bitmap mailBmp;
        //private static Bitmap calendarBmp;
        //private static Bitmap contactsBmp;
        //private static Bitmap tasksBmp;
        
        private static Image savePng;
        private static Image cancelPng;
        private static Image editPng;
        private static Image newPng;

        #region png base 64
        

        private static string savePngEnc64 = @"iVBORw0KGgoAAAANSUhEUgAAAB" +
            "gAAAAYCAYAAADgdz34AAAE3UlEQVR42rWVa2wUVRTHz53Z97MvRExq5KNp" + 
            "NDHRBBM1bdNiC7VQEBHxgegHSJRHWyulLTNLH0Zogoii1je+kWobkRaQWt" +
            "m2tIB9kfhdvlATQ9vthrqP2es5985ud4v96GTP3Htn7pzfOf977l0G//PF" + 
            "6FZbU3OvoqhPxWJR8PmzoLB0Dfi9PlBVBWcw/ElTFIWGABx/PCEMb8ATCQ" + 
            "iFw3C+92eYnZ4G1aJCdnb29w2NjX8IwM4dO7Spqb80m83G8vPvhl2vN3G3" + 
            "w87MIDgzA5EgHAse44psAVseno+wN5s1mAuFOE31+/36ofbDAfHhy9tf0s" + 
            "LhsEYRrlhxF1Q36DB3K3p7qmyhz8wH1BIky2OHVq2RAGKOx+MJHDn6lgRs" + 
            "3/aiho0myFlZsE9r4VluRyoD6SkF4dInS2WGIvGZ8D+s5UADhGZnOb10u9" +
            "362+8ck4Btz7+QAWg82MZzvM5MgNkh0VF2xrlsE3hD46FbEfaGJgE03+1x" + 
            "6+8ePy4Bz219VkMoAYAA+wNtiyTipnNzgZNPxGLLvtdlg0OBRgKIaZhB4L" +
            "0P3peArVueERlQan6sojq9lXuctswM+NIZUG8+GmftBwkwIyRyuVx6x0cf" + 
            "SsCWzU9nSFR7oJU7bdYMAE9mIn4ENAHms2jcYEeaFyQiwMeffiIBm5/cpI" +
            "Epkc/vh+qmNgjNR4GldM/s8DSpTKiQ6GhLQ6qKnE5n4LMTn0vApg0bZQaY" +
            "ms/nh91NrSJNtuCHpelPHpmQn6QyM6A5x1obIBzCDPBTBOgnvvxCAjasr9" +
            "KYlAgsVisUV2zEXayCBY1asYPFrlIEJ5FICO8GtkbcgLgRh4RhQN/pThzH" +
            "xRwHZvDVN19LwPrKdak1SNedLSrTRWWbXr588bcOh0P/9uR3ElC5tiIlUb" +
            "JQ/rOfPl5qjtl32O36yc5TElBRvub2DHCSIg84jjIxKZM47Ehxs0w5I5lQ" +
            "HjzvEoykS2ZgR0Bn148SUL768SQAkofanppqyMvLEwEtddG7zlOd0N/XJ9" +
            "YDD0vwejziHfYD3ad/koDSouJkmYqx1Wrlu/buYTPTM3D9+p8iuvz8fLhn" +
            "5UrhVxxEeBmGwS6c/wUGgkGOABbHBXY6HCID9KGfOdsrAYWPPpYhEdJ5zW" +
            "u1bHBwEM71nuX4ISsuKYHKdZVif9EYTbS/9ffDyPAIR5lYLBYT35IfGwLO" +
            "9V2QgEdWPZzaaKJUsTTr9tfDEAJ6z/QARgrFpSVQVlYGFCUZPaMWo4ffr1" +
            "wVpUtj1B4sFgv9CQV+DV6UgFUPPpSZgd3O6+r3sYGLQejt6RFSFBYXQUlp" +
            "KTkRY4yWo7FLg0MwPjaWWmQsz+Qm1YOXhiTggfvur1ZUtT0JwBLju6v3sm" +
            "BwALq7u0S1lJWVQ2FRIZBjkgYhAjAyPAzXJiYQwEWdWlEirD6C6VfHxyTA" +
            "5XS5li9b9oPP611tniOw89VXRCt2LMoRiUSEpctDEV+5fBmujU8IaamUSR" +
            "66/r558/CNqRv1ybq3oRXdecfyZlwcb05OjrK28gnc7U6abcGAVHSq0nEQ" +
            "x8jxmUFrjeP45ORkdGJ8PEJZyv9psVdi07OzHaG5uY70I8CNlo2mFhQUWK" +
            "uqqty5ubkqLpqKJaeQA4yYm/uC1sEIhUKx0dHR+a6urghmpKT5MtBm0ML/" +
            "ArBlxEPZ/ksHAAAAAElFTkSuQmCC";
        
        private static string cancelPngEnc64 = @"iVBORw0KGgoAAAANSUhEUgAA" +
            "ABgAAAAYCAYAAADgdz34AAADHklEQVR42rWVX0iTURjGn21sOl1KhtKIGi" +
            "YIda033XmlFxpW9Mf+meSdddWdNyKRUREVTuzPyqTC20gpLWpFdaX3gTRt" +
            "iWiahrY5t7nZcz7fzc/TRAt84eE75+zb8/vOe95zjgVbHBatv486Te3M8N" +
            "tGsUxNUk+pL5kAZ4sBXy1g97CTk0FOeTFs0oJJY9QLID4KnGfziRngdgMB" +
            "Hz0OsJNNJddRQpN5LEp9pS4CkQmghM2JFKC+CXh8W/6wpJksrdPO9JvKEz" +
            "8Uj4BzfHSnAE13AW+DvGg1JVVXcp3xGBWUWbykrgMX+OhIA+4TcEpyZvvP" + 
            "ihmnRgVwUwd0mgDGDGw22FtbgcVFxNva+OnJtW5WK+zNzVywbMRbWjj1BH" + 
            "7IGryibumADgJOmkrLVlYG1+Cg0Y91dSHS2LgKobnT54OjocHohsrLkRga" +
            "wpQABqg7OqCdgDrJpzHocMDV34+sigrjhSghIQVhuGieJeZRvx+hqip+Rc" +
            "wABKjXVLsOINF73AxQ7Zwc5Pf1wSmQCCEqnGIeoflcdTUsCwtGXwFGqDfK" +
            "WQcwZ95jUiXm3ZfMzUVBby9yBZKKMM1na2pgDYfTY9Myg7dUpw7gqnuPCk" +
            "CPhMuF3YEA7EVFRj8+NYWxkhLYQqE1703LDN5R93TADQKOYLXOU2HhghYy" +
            "53mSllTMM13TXJNlU3X9lDL1Uw90wDUCDmNlo6UBNHfTPF/MZ/1+41kg6Z" +
            "ojZMJUXQrwjXpPPdQBVwk4hNXtrsx30Xy7mM/Q/DtzrmIP12SHQH4RMi4Q" +
            "BVC7+QPVpQOuEFCr8iuAPFbHXhoZuaX5KPsOqZYYq6uY1VUokBGC59lPAT" +
            "5S3TrgMgEHBWBM2ONB6cAAfg8PI1hXB4epWgwIq8vT04NtpaUYrqykcxCz" +
            "AvgE46xeC+Ch4FUJiJkqSdWIncpC5ojKB7mkP4OVO+Ez9UwD1F/icX1C/p" +
            "BeB4nlDOb62JKYq3mqffBcO67dvB0CPMOdVmz+Lsg0tgjjJI1MaheOijP7" +
            "WV28kO2F+Pu22kxbLTKPiXgww5WZitSl78a/x4aX/pbEH2Ceaij62q1HAA" +
            "AAAElFTkSuQmCC";

        private static string editPngEnc64 = @"iVBORw0KGgoAAAANSUhEUgAAAB" +
            "gAAAAYCAYAAADgdz34AAAFCUlEQVR42q2Va2xTZRjHn+f00N3ILtlaJiuw" +
            "rAJuwW0gEDUSEwhRTMyEQCCIEzVELhGBjWvYTECHyz6QTAN+IaIfNEQhEa" +
            "IJ4+IYhouJQEcHGzvdSrdurN2t13Pa057H9/TmWjbCB5/k3M/7/z23930R" +
            "/kdrXbdiWbY//TynKNcpHN65sKXFivGPGo1GX1dX9wURJY9CBHwO8ZC3Gz" +
            "cvaFsf+q04z2eyo1eWB0GW35g41jg2Ni6IkgQch4RRY/pJ9yqMIPIUvaoD" + 
            "wwEnKea1ODDohJrGNNpqG8XpPh+EeP5CEmB83NUtBVQABwnBiUc8oqhu5F" +
            "kJixBs3wCi2woffKWBXnsAZvJ22J8JZOrE80kAl8stBIKBiNfcJBFA/FnV" +
            "ZlegMIrmraAR79InjRyaHomgSHY6vV8OiyJvWntIrkoCuD0eIRhgAI6bPE" +
            "WRADCRIv+jI5ghXoHaExxdujmCotcBJz73U2G+xrb6kLxmcIS6JgJKPCpA" +
            "lhMp4SZJU7zoUt8PkOb6GY7/mgmnzwng97ngaPUorHytAFbudHzYZaOL7L" +
            "exJIDX67WoAC7udbQWTxU56LyCvONbOntDh0eab0IgINH2d57gprczYaSg" +
            "iSqXfbqc6f3DDm8ywOezyHEAF6nCf7AYIOS+T9DfiLeFmbSjvgUDLKVVSw" +
            "eodoMGh7JqAAtW06JFi15lenfU7k0C+Hw+IRQKRT1l3qemiBUQlMcNIDjy" + 
            "4aN9F8Ht9sLr85zQsEWG4bSN4J5eDYWFhTB//vwFTO9BvNMSAL/fb4kAWJ" +
            "ETXRS7V2QXhKzHYNiTQdV7r6o9T2UGNzbv8II/YzlZue2ois+ZM4f0ev08" + 
            "RVGEpwGiKISjgIT36pwAJQhBaxNIQYTN+1uhS7CDIV+EkztHgc8ph3v+Xa" + 
            "DTvwAlJSWRCNLT0+cygOUpgCiKlnA4nBwBAgWsx9k5BNvrr8GtO1bKmy7j" + 
            "yc+GSV9owGvOXZBXMIuMRiMWFxdDWloa8Tw/BUCSLIoKUCdarMgB23fEgw" + 
            "sPNF6HP1p7IUOrUPM2J5bNzaHfrdswM9eoek6lpaWYlZWl6kwNkBggrCiR" + 
            "zlEBkv0MaMVb1HTKjD+eH2DvCY59PErLF/N4rrOaKKMUWc6hoqKCcnNzYx" +
            "FzpNVOmwIQCAhqBPG1aMD8E/zZ8j00nBpQHYPatS54f2UYzpqrYJwqoaio" + 
            "CCorK0Gn0wETjK7K/DTIy82ZHMB62qL+qKbI73qCQ9ZWEDr/pqPNV3HVK2" +
            "Owb4MEF8xvkuBeqnYMlZeXo06vB6/HQ6xBkKUG/rpxs3Xvnt3vBYNB15QA" +
            "FgENCm3osLaAZ+QBiX4XLpzVB53D88A0sopmz56NBoOBsrOzsb/fDqwxWG" +
            "vqsKPD3F5TU7vK6XQOJLaTFICgsA1HbdFT3xyAsiI2WPaDop0LmpwloDe8" + 
            "DPoZM0Cr1bJJ5oGenh7IL8iHF41GuHz5knX3nj0rhG6hN2m/So1A3dEeW6" + 
            "20es0a3Lx+GWzctIVyZ7yErP3UXQ/Yd2ITEm19ffTwYScuXbIYzOb7toMH" +
            "D75lMrV3pu50yYBg0EIsRaZ7d0nD81hRUZlY7KJ7DarFJLYoosXSQ0MOB0" +
            "qSv6vu8OF3OzoedMMklgoQKFrk5CU6Jh4DAANA72MbtLVdu93U+PU6Vod+" +
            "mMImAorq6+u/jL+LC6eamsJgKAyDQ47xc7+cafC43Q54hqWqcPD8RrHjmf" +
            "YvaouoLYYI+sEAAAAASUVORK5CYII=";

        private static string newPngEnc64 = @"iVBORw0KGgoAAAANSUhEUgAAABg" + 
            "AAAAYCAYAAADgdz34AAAC80lEQVR42q2W70tTURjHn+O9m3NFwQqbjWSsR" +
            "rYg6FUE6RCt7I1Ehr1ab4IIol71LvoXhN4FgYKuIGi4kHQFvVBfCFtbsdi" +
            "cDpcYY2NuVyTnft17T8+9bWOTw/SWDxzO9znnufuc5znn3jMCDZbL5UYII" +
            "Q+wETiAUUphfn7+9+Tk5KPp6WmBFUP2AGaxu6U82zBH98TVfLVfXFyEiYm" +
            "JIK7putfr3WoJyGazcxg4hCujtSwadaNf64PBIHg8HprP50MKZGpqaqslA" +
            "LubBylPzaLRKPh8PigUCkoLGY3GG2NjYwITsLm5qQCGtJSoUqkoECpJkhp" +
            "jMBhmnE7nMBOQyWQ0Axgx/s7OzitMQDqd1lwihgXMZjMbkEqlDiWDrq4uN" + 
            "iCZTB4KwGKxsAEbGxvKMf2vEuHpDXR3d7MB6+vrmjMokRwk2z7hO02JRR4" +
            "EAz3lt1qtbEAikdC8yWvUDTHplarPcffhPHkYsNlsbEA8HtecwQp9DWvyG" +
            "1VbyR1wtD312+12NiAWi/0DYByzeIuaIOA2OMhjf09PDxsQiUT2BYiwS4u" +
            "QrQN+Ui/8oj5Vnyb9YCXDP1bl9/eEYgJEuZRqAoTD4X33wM+/gCLJ1v2yl" +
            "IeSuK1qPX8M2rmj9TmZis2AUCikfk1bZTAnuagIhVbvQYMmtAkQCAT2LdF" +
            "X+SUV6EodIEIRy1ZSNQd64KG9XIYdgarT9FsTYGlpSfMxXZY9sCp9VLWtb" +
            "RAc3N3AtatO9ibj7aT5FEWlD7Ai/d3ks1w/XOJG/b29vWzAwsKC5hstXJ6" +
            "B5cpnFWDXOeGybsTf19fXDMBLm0PjTSbT846OjifV8b2NZTRS/sJ9L80aF" +
            "OeifqBwAQbGBUF4JqK5XC6JuN3uMzg3igty4ILMqI+jNqJWHtKh5lFztQU" +
            "p/ySqCSi9JEEFsnziJM5JJ0RrnKP6DI6nle3BmHcKgEfHhM5xnFAO8ZEGg" +
            "B61DjVfzYKv/rBYLZGIYxWUZdRF1Luo86h3UG+jFv4A7gWzwtP8PLUAAAA" +
            "ASUVORK5CYII=";

        #endregion

        /*      Imágenes bmp base64.No se utilizan
        private static string mailBmpEnc = @"Qk32BgAAAAAAADYAAAAoAAAA" +
            "GAAAABgAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7uv4yFvouE" +
            "vYmDu4eBuYV/t4N9tYB7s355sXt3tntxsnhwsHZurXNsrHFrp21pomln" +
            "oGdlnmVjnWNi7u7u7u7u7u7u7u7u7u7uwY6H9N3C/vDa/ejM+tSp+cye" +
            "98OS9bqI87F/87KA8qt68KRy76Bu7phl7ZVi7JVi54xb0ntSoGVj7u7u" +
            "7u7u7u7u7u7u7u7uwo+Ix6WZ9ujb//bn/u/b/OfL++HA+9iv+tev+tSr" +
            "+tKo+cyg+Mic9b+S9LqM8al35ZZmoXBSoWdk7u7u7u7u7u7u7u7u7u7u" +
            "xZSN9ejayaOY9uvh//js/vXo/e/a/evS/OfL/OTF+927+9u1+tSq+tKl" +
            "+cqW8buFqHZa7JVdn2hm7u7u7u7u7u7u7u7u7u7uxZSN//ns9ercxqKV" +
            "+O7k//nw//fu//fr/vTl/u/c/uvU/eLB/N+7+tev8b6Hq3pe+Lh6/69q" +
            "oWpp7u7u7u7u7u7u7u7u7u7ux5eP//vv//vw9OncxKOX9+7m//v1//nz" +
            "//jx//bs/+7Z/erQ/eTC8syiqntg+M6a/9Oh/8CFom1s7u7u7u7u7u7u" +
            "7u7u7u7uypqS//z4//v28ebYza2evp6V7+Lb8uzl8+3l8uvk9Ore9ejZ" +
            "6NO/poVyt4xx5b2T/9ap/8ybpXFw7u7u7u7u7u7u7u7u7u7uzJ2V///8" +
            "7uPbzaye/fv6/fv5v6GTvKCQvJ+Nu5qIupmJt5uKsZWE+u3e+unYo4Jq" +
            "572O/9KjqHd17u7u7u7u7u7u7u7u7u7uzqCX7OHbzKud/fr58url5tnQ" +
            "5dfO5dfN5dfM5NbM49TI3Mq+3Mm93Mm8382/+unbrIJp57WGrH597u7u" +
            "7u7u7u7u7u7u7u7uz6GZ0LGj6uHa/fv67OLc6+Hb7eTd7ePd7eLb7eLb" +
            "7uPb9e7l9evk9uvj9+zh/PHlzr61r4VtsoWC7u7u7u7u7u7u7u7u7u7u" +
            "0aKXfdb/8uzn///////////////////////+//37/Pn3+fXv9fHp8+vl" +
            "8enk7uXe1si/L6f3tYeC7u7u7u7u7u7u7u7u7u7uy6mkdM3/+vf1////" +
            "/////////////////fz8/Pv69fPx7vDw7O/w2OfvxN/swt/thMXnK6ft" +
            "r4SC7u7u7u7u7u7u7u7u7u7u1KeevuX6nNf2rN/7nNz9h9X+b8z+VMb/" +
            "RsT/RsT/PsP/Obz7NLT2MbL0K6vuJ6XpKKfrLqrvr4J/7u7u7u7u7u7u" +
            "7u7u7u7u28jC0rSo0O36yOv7x+r7v+f8suP9o97+jtj+idn+htX/e83/" +
            "a8f/XcD/Tbn/SLf/R7b/rJKJ7u7u7u7u7u7u7u7u7u7u7u7u7u7u6ePi" +
            "07ew29zd1e/61O/60e77y+z8vun9reH+mNn+gdH/ccj/ZMP/Vr3/t7/C" +
            "uKCX7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u6uXk2b621rux1fH6" +
            "3fL61/L7y+/8u+f9pt7+jNT+cMn/XcH/rZyW1c3L7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u6N/d07Op3d7f2/P60vD7xez8" +
            "r+H9k9X+kr/cv6Wb7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u6uXk2L612cG31e73yOv7seL8uKWezLex7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u6eHg1LOpzaeayaqh4dvb7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u";

        private static string calendarBmpEnc = @"Qk32BgAAAAAAADYAAAAo" +
            "AAAAGAAAABgAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7uuKOUkXxqemNO" +
            "alM8YEgwYEgwYkoxYUgwYEgwY0szYkoxYEgwYEgwYEgwYEgwZEszYUgw" +
            "YEgwY0oyYUgwYEgwYEgw7u7u7u7uuqWW6tPE27qlxKCLu5d837OUzKGC" +
            "t41xsYpt37OUzKGCt41xrodq37OUzKGCt41xrodq37OUzKGCt41xrodq" +
            "Zk447u7u7u7uvKaX//fz/+3i/NvFwJl6/+nb/d3H+c+1vJV2/+HP/tW8" +
            "/MWluZJz/9vG/9Gw+8OfuJFy/86y/sKf97eSuJFyYEgw7u7u7u7uvaiZ" +
            "//j2//Hq/OTY0qiL/+7h/+TR/N3J0KWI/+TU/9vE/9S4zqOF/+LR/9zE" +
            "/sywzaKE/9e//8yt/sWizaKEYEgw7u7u7u7uv6qb//v5//j1//bx5Lug" +
            "//Ho//Dl/+nb5Lqg/+fZ/+LQ/97I5Lqf/+XU/+HN/9zF5Lqf/9bC/9S7" +
            "/9C05LqfYEgw7u7u7u7uwauc4bib1ayOxZyBvpd74LaY0qiKwJd7uJF1" +
            "4LWWz6WGvJJ2D0XuBTnjBjXQAiy8ACm137OUzKGCt41xrodqYEgw7u7u" +
            "7u7uxK6f//v6/Ozg/eHQx6CC/+/m+uHQ+tjDwpt9/+re/+DM/9O4I1Tz" +
            "/9zG/9O5/86wASu4/9fC98Sm87uYuJFyYEgw7u7u7u7uxrCh//39//n0" +
            "/One1qyQ//v4/vHn+uHR06mM//Pt/+rc/+HMPGr0/+TU/93H/9i/AzHJ" +
            "/+LP/9i++curzaKEYEgw7u7u7u7ux7Kj//39//38/fbw5Luh//z7/vj1" +
            "/uzh5Lug//fx//bv/+vgWoH2/+/m/+vd/+PQAzTa/+XU/+XU/97J5Lqf" +
            "YUkx7u7u7u7uybOk47yh3rab1q2SzaWL4bqe2a+SzKKHwpqA4bmd1q2O" +
            "x52CbY/5WYD3OWb0IFP0Aj3t4Lib06iJwJV5sopwYEgw7u7u7u7uzbep" +
            "//39//n1//Dn1a2S/vv6+uvh9+DPyqKG//Tt+eXX+eDSxJx//One+tzH" +
            "+dC3v5d6/9/K+9C1+8akuI9yalM97u7u7u7u0Lyu//39//r3//f03bWa" +
            "//7+/vXx+urg1q2R//f0/O3i+eTV06mM//fy/+7i+NfCz6WI/+zg/+DM" +
            "/9CzyZ6Adl5I7u7u7u7u0sCy//38//39//395L2j//7+//79/vn45L2j" +
            "//38//n2/vLr5L2j//37//n2/unf5L2j/+/l/+nc/+DN5L2jf2dS7u7u" +
            "7u7u6KaG6KaG6KSE56OB56B+55565pt25phy5ZVt5ZJp5I5k5Itf5Iha" +
            "44RV44FR4n5M4ntI4XhE23I812w00mkzyGAo7u7u7u7u6amK/93L/dfD" +
            "/9a+/s+2/syy/sis/sSl/cGh/byb/LeV+7SO+rCJ+auC+ad9+KN3+KBx" +
            "95xu95pq9pdn9pVkyGAo7u7u7u7u662P/+LQ/t/M/t3K/drG/dfC/dK8" +
            "/c63/Mqx/Mer+sKk+r6f+bqY+LaT+LGM96yG9qmA9aV69aB19J1w9Jps" +
            "yGAo7u7u7u7u7LKV7LKV662P6aeG56F/559855x45pp05pdw5ZRs5ZFo" +
            "5I5j5Itf5Ihb44VW44JS4n9O4nxK4XpG4XdC4XU/2Ws27u7u7u7upJqU" +
            "////pJqU////n5aQ////mZCL////kYmF////iIJ+////fnp2////dXJv" +
            "////bGpo////ZGNi////XV5c////7u7u7u7u7u7uICUg6enpICUg6enp" +
            "ICUg6enpICUg6enpICUg6enpICUg6enpICUg3t7eICUg0NDQICUgvb29" +
            "ICUguLi4ICUg7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u";

        private static string contactsBmpEnc = @"Qk32BgAAAAAAADYAAAAo" +
            "AAAAGAAAABgAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7uuqWWh3FeeWJN" +
            "alM8YEgwYEgwYEgwYEgwYEgwYEgwYEgwYEgwYEgwYEgwYEgwYEgwYEgw" +
            "YEgwYEgwYEgwYEgwYEgw7u7u7u7uuqWW//bx7t3T7trO7tfI79LB7865" +
            "8Mqy8MWq8cCi8buZ8raR8rKJ862C86l69KV09KFt9Z5o9Zxk9Zph9Zph" +
            "YEgw7u7u7u7uu6aX//n2//fy//Xv//Lr//Dn/+3j/+re/+ja/+XW/+LR" +
            "/+DN/93J/9rF/9jB/9W9/9O5/9G2/8+z/86x9ZphYEgw7u7u7u7uvKiZ" +
            "//z6//r30M/SGlmtA0uuA0WfAz+RATmHADV9zr23/+PTs4t1roRuqX1m" +
            "pHhgoXRcoXRcoXRc/9C09Z1mYEgw7u7u7u7uvqqb//79//z7FFe3N3rV" +
            "SYrkQ4XeA0OaGW3eC02nGkF4/+fZ/+TV/+LQ/9/M/9zI/9rE/9fA/9W8" +
            "/9O59KFtYEgw7u7u7u7uwKyd//////7+E1i3Z57pUZDoTY7nA0SbGGze" +
            "E2DIAzmD/+vfuZJ9s4t1roRuqX1mpHhgoXRcoXRc/9a99KV0YEgw7u7u" +
            "7u7uwq6f////////GF69gKvkX5flA0WeGW7iA0+3GWzgEk+j/+7l/+zh" +
            "/+nc/+bY/+TT/+HP/97L/9zH/9nD86t9YEgw7u7u7u7uxLCh////////" +
            "tcDPE1StGly1yNXZ+fryBVG5BkGOz8rK//LqvpiEuZJ9s4t1roRuqX1m" +
            "pHhgoXRc/93I87CHYEgw7u7u7u7uxrKk////////9fX1zdDTYWZsVFVW" +
            "YWFhBkunubzC//jz//Xw//Ps//Do/+7k/+vf/+jb/+bX/+PS/+DO8raR" +
            "YEgw7u7u7u7ux7Sm////////xMTEAAAAwcHBoqKihYWFVVVVn56d//r3" +
            "//j0//bx//Tt//Hp/+/l/+zh/+nd/+fY/+TU8b2cYEgw7u7u7u7uybao" +
            "////////ODg4LCws1tbWwcHBoqKihYWFZGRk//37//v5//n2//fy//Xv" +
            "//Lr//Dn/+3j/+re/+ja8MOmYEgw7u7u7u7uy7iq////////U1NTSUlJ" +
            "tLS01dXVwcHBoqKidXV1///+//38/LeL+7SI+q+D+ap++KV496Bz9p1w" +
            "/+vg8MmxZk437u7u7u7uzbqs////////fn5+Y2NjXV1dbW1tWFhYwcHB" +
            "hISE//////////79//z7//v4//n1//bx//Tu//Lq/+/m78+6b1dB7u7u" +
            "7u7uz7yu////////xcXFbGxsgoKCoaGhjo6OVVVVra2t/////////LeL" +
            "+7SI+q+D+ap++KV496Bz9p1w//Pr79TEeWJN7u7u7u7u0L6w////////" +
            "8vLyuLi4jY2NiIiIhYWFtLS08fHx//////////////////////38//z6" +
            "//r3//j0//bw7tnMgm1Z7u7u7u7u0b+x////////////////////////" +
            "//////////////////////////////////////79//z7//v4//n1//fy" +
            "i3Zj7u7u7u7u0sCy0b+x0L6wz72vzrutzLqsy7iqybeoyLWmxrOkxLGi" +
            "w6+hwa2fv6udvqmbvKiZu6aXuaWWuKOUt6KTtqGStaCR7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u" +
            "7u7u";

        private static string tasksBmpEnc = @"Qk32BgAAAAAAADYAAAAoAAA" +
            "AGAAAABgAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAA7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u3N3fkXxqeGB" +
            "LalE5cVc/c1lBf2JKl3RZiWhMe1xBb1M5aE42Zkw0Zk01aE42YUkxYkk" +
            "xYkkxkod97u7u7u7u7u7u7u7u7u7uv62e+ezi69nQ7M6+7Miz6L+prI+" +
            "rWFKqmHyk7rWU77OO77CM8LCK8bCH8a2F8qp98aV13pZnZkwz7u7u7u7" +
            "u7u7u7u7u7u7uv62e//37/vr3/vTw+uvkwLnaJDO/BhmzJjTDybTG99K" +
            "8+NC49syy9sms9sao9cOl8LiV8KR0aU417u7u7u7u7u7u7u7u7u7uwK6" +
            "f//7+//z7/Pj3xcTmLDvBFynIMEXkIDbXVGHO6dHO+tzJ+tjD+dW++dK" +
            "5+c+19sWn8qd2Y0kx7u7u7u7u7u7u7u7u7u7uw7Gj/////Pv9wcTsKDj" +
            "FFCnMRVjuZHX1PlPoFzHSeHPJ+d3O+tzI+tjD+dS9+dG59seq8ayEaEw" +
            "07u7u7u7u7u7u7u7u7u7uxrWm9vf9pKzoJjjNGi7TR1nreoj/j5v8ZXf" +
            "0N0zlJjTPv7LP+t7O+tvI+tfC+dS998qv8K+LaU427u7u7u7u7u7u7u7" +
            "u7u7u08W619v5Kz7XJjrfTl/ze4v/tLr59/Dyoab2Znb0LUPmNUTO1sX" +
            "R+9/N+tvH+tjC99C277GNbVI47u7u7u7u7u7u7u7u7u7uy7qt6+3+kp7" +
            "4UGT4c4P/sbj+/Pr5/vj16ePykpz5Y3T0KDzfbHHR6dTS++HQ+tvG+dn" +
            "D8bqabVI57u7u7u7u7u7u7u7u7u7uybeo////9ff/qLL/wsr//Pz///3" +
            "8/vv5/ff09e7wpav4YnP0KDzajYzQ6NPT/N7O+NrG8b+idFlA7u7u7u7" +
            "u7u7u7u7u7u7uyriq////////+Pn//v7///////////38/vr2/vj08er" +
            "wmaL6WWvyMELXmJbS9OPb+d/Q88mxc1Y97u7u7u7u7u7u7u7u7u7uzLq" +
            "s//////////////////////////7+//36/vr3/vn0+fDvsLT1WmruNUb" +
            "Qta/V+uXX9M+6hWdP7u7u7u7u7u7u7u7u7u7uzryt///////////////" +
            "///////////////7+//79//v3/vj0+O7wurz0TV3sSFTS08fa9t7PmXl" +
            "h7u7u7u7u7u7u7u7u7u7uz72v/////////////////////fj1+Orh9uH" +
            "W89TD88+78sew9NK99drNm5/xVWbwYWvV6dzgrIlw7u7u7u7u7u7u7u7" +
            "u7u7u0L6w////////////////X4ycVYGSTHaIS21/SGN0SGJySF9vdnd" +
            "9pZWQ89TDt7n1TF3oZGvOuJ2L7u7u7u7u7u7u7u7u7u7u0b+x///////" +
            "/////////dJmou+Xsmd3ofs/fdcXVcMHSbrrNbbHCbHJ69dTD++3nw8H" +
            "uNUjqZGjJ7u7u7u7u7u7u7u7u7u7u0b+x////////////////i6q2pMn" +
            "StuzzYYycdLG+fs/ed8jaXY2eRl1t8uDU++je/O7mzsnmUFCX7u7u7u7" +
            "u7u7u7u7u7u7u0sCy////////////////3ufqdZuqx+30V3aFXoCPaZW" +
            "kjdDeTWx8v7Ko/vr2/fTv/fTu+/XxnZSu7u7u7u7u7u7u7u7u7u7u6ur" +
            "r2Mq/0b+x0L6wz72vyLeqc5qpoMHLxfD3v+zzruXvkMjVaX6GwK6ewrC" +
            "hwa+gwK6fv62e4OHi7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u5ObnfKa1eaOxcpyrcJWkboiW4+Pj7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7" +
            "u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u7u";
*/
        #endregion

        #region Public properties
        public ToolbarStates CurrentState 
        {
            get { return _currentState; }
        }
        #endregion

        #region Constructor

        // Static constructor to initialize
        // the form's static fields.
        static StackView()
        {
            // Create the static bitmaps from Base64 encoding.
            CreateBitmaps();
        }

        public StackView()
        {
            InitializeComponent();

            // Assign icons to ToolStripButton controls.
            this.InitializeImages();

            // Set up renderers.
            //this.stackStrip.Renderer = new StackRenderer();
            this.stackStrip.Renderer = new StackRenderer(new CustomProfessionalColors());
        }

        public StackView(bool read, bool create, bool modify)
        {
            _read = read;
            _new = create;
            _modify = modify;

            CreateBitmaps();

            InitializeComponent();

            // Assign icons to ToolStripButton controls.
            this.InitializeImages();

            // Set up renderers.
            //this.stackStrip.Renderer = new StackRenderer();
            this.stackStrip.Renderer = new StackRenderer(new CustomProfessionalColors());

            ConfigureActions();
        }

        #endregion

        #region Private members

        #region Layout

        public void InitializeGeneralStyle()
        {
            BackColor = ETNIA_RED;
            stackStrip.BackColor = ETNIA_RED;
        }

        // This utility method assigns icons to each
        // ToolStripButton control.
        private void InitializeImages()
        {
            //this.editStackButton.Image = mailBmp;
            //this.newStackButton.Image = calendarBmp;
            //this.saveStackButton.Image = contactsBmp;
            //this.cancelStackButton.Image = tasksBmp;
            this.editStackButton.Image = editPng;
            this.newStackButton.Image = newPng;
            this.saveStackButton.Image = savePng;
            this.cancelStackButton.Image = cancelPng;
        }

        // This utility method creates bitmaps for all the icons.
        // It uses a utility method called DeserializeFromBase64
        // to decode the Base64 image data.
        private static void CreateBitmaps()
        {
            //mailBmp = DeserializeFromBase64(mailBmpEnc);
            //calendarBmp = DeserializeFromBase64(calendarBmpEnc);
            //contactsBmp = DeserializeFromBase64(contactsBmpEnc);
            //tasksBmp = DeserializeFromBase64(tasksBmpEnc);

            editPng = DeserializeFromBase64Png(editPngEnc64);
            newPng = DeserializeFromBase64Png(newPngEnc64);
            savePng = DeserializeFromBase64Png(savePngEnc64);
            cancelPng = DeserializeFromBase64Png(cancelPngEnc64);
        }

        // This utility method cretes a bitmap from 
        // a Base64-encoded string. 
        internal static Bitmap DeserializeFromBase64Bmp(string data)
        {
            // Decode the string and create a memory stream 
            // on the decoded string data.
            MemoryStream stream =
                new MemoryStream(Convert.FromBase64String(data));

            // Create a new bitmap from the stream.
            Bitmap b = new Bitmap(stream);

            return b;
        }

        internal static Image DeserializeFromBase64Png(string data)
        {
            // Decode the string and create a memory stream 
            // on the decoded string data.
            MemoryStream stream =
                new MemoryStream(Convert.FromBase64String(data));

            // Create a new bitmap from the stream.
            //Bitmap b = new Bitmap(stream);
            Image i = Image.FromStream(stream);

            return i;
        }

        private void StackView_Load(object sender, EventArgs e)
        {
            // Dock bottom.
            //this.Dock = DockStyle.Bottom;
            this.Dock = DockStyle.Top;

            // Set AutoSize.
            this.AutoSize = true;
        }

        // This method handles the Click event for all
        // the ToolStripButton controls in the StackView.
        private void stackButton_Click(object sender, EventArgs e)
        {
            // Define a "one of many" state, similar to
            // the logic of a RadioButton control.
            foreach (ToolStripItem item in this.stackStrip.Items)
            {
                if ((item != sender) &&
                    (item is ToolStripButton))
                {
                    ((ToolStripButton)item).Checked = false;
                }
                else
                {
                    switch (item.Name)
                    {
                        case "editStackButton":
                            ConfigureByState(ToolbarStates.Edit);
                            this.OnEditButtonClick(EventArgs.Empty);
                            break;
                        case "newStackButton":
                            ConfigureByState(ToolbarStates.New);
                            this.OnNewButtonClick(EventArgs.Empty); 
                            break;
                        case "saveStackButton":
                            this.Validate(); //Los toolstrip por defecto no lanzan los validate de los objetos cuando se pulsa en ellos
                            this.OnSaveButtonClick(EventArgs.Empty); 
                            break;
                        case "cancelStackButton":
                            this.OnCancelButtonClick(EventArgs.Empty); 
                            break;
                    }
                }
            }
        }

        #endregion
        private void ConfigureActions()
        {
            if (_read == true && _new == false && _modify == false)
                ConfigureByState(ToolbarStates.OnlyRead);
            else if (_read == true && _new == true && _modify == true)
                ConfigureByState(ToolbarStates.OnlyEditNew);
            else if (_read == true && _new == false && _modify == true)
                ConfigureByState(ToolbarStates.OnlyEdit);
        }

        /// <summary>
        /// Muestra los botones correspondientes en función de cada estado.
        /// </summary>
        /// <param name="state"></param>
        private void ConfigureByState(ToolbarStates state)
        {
            _currentState = state;
            switch (state)
            {
                case ToolbarStates.OnlyRead:
                    stackStrip.Visible = false;
                    break;
                case ToolbarStates.OnlyEdit:
                    stackStrip.Visible = true;
                    editStackButton.Visible = true;
                    newStackButton.Visible = false;
                    saveStackButton.Visible = false;
                    cancelStackButton.Visible = false;
                    break;
                case ToolbarStates.OnlyEditNew:
                    stackStrip.Visible = true;
                    editStackButton.Visible = true;
                    newStackButton.Visible = true;
                    saveStackButton.Visible = false;
                    cancelStackButton.Visible = false;
                    break;
                case ToolbarStates.Edit:
                case ToolbarStates.New:
                    stackStrip.Visible = true;
                    editStackButton.Visible = false;
                    newStackButton.Visible = false;
                    saveStackButton.Visible = true;
                    cancelStackButton.Visible = true;
                    break;
            }
        }

        #endregion

        #region Protected virtual

        protected virtual void OnEditButtonClick(EventArgs e)
        {
            EventHandler handler = this.EditButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNewButtonClick(EventArgs e)
        {
            EventHandler handler = this.NewButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnSaveButtonClick(EventArgs e)
        {
            EventHandler handler = this.SaveButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnCancelButtonClick(EventArgs e)
        {
            EventHandler handler = this.CancelButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion

        #region Public members
        /// <summary>
        /// Restaura la botonera a su estado inicial con los permisos que se han indicado en el constructor
        /// </summary>
        public void RestoreInitState()
        {
            ConfigureActions();
        }
        #endregion

    }

    /// <summary>
    /// tabla de colores para el toolstrip que compone la botonera
    /// </summary>
    internal class CustomProfessionalColors : ProfessionalColorTable
    {
        private static Color ETNIA_RED = Color.FromArgb(221, 34, 29);

        public override Color ToolStripGradientBegin { get { return ETNIA_RED; } }

        public override Color ToolStripGradientMiddle { get { return ETNIA_RED; } }

        public override Color ToolStripGradientEnd { get { return ETNIA_RED; } }

        public override Color MenuStripGradientBegin { get { return ETNIA_RED; } }

        public override Color MenuStripGradientEnd { get { return ETNIA_RED; } }
    }

    /// <summary>
    /// Render para el toolstrip
    /// </summary>
    internal class StackRenderer : ToolStripProfessionalRenderer
    {
        private static Bitmap titleBarGripBmp;
        private static string titleBarGripEnc = @"Qk16AQAAAAAAADYAAAAoAAAAIwAAAAMAAAABABgAAAAAAAAAAADEDgAAxA4AAAAAAAAAAAAAuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5uGMyuGMy+/n5+/n5ANj+RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5wm8/RzIomHRh+/n5ANj+RzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMzHtMRzIoRzIozHtMANj+";

        // Define titlebar colors.
        private static Color titlebarColor1 = Color.FromArgb(89, 135, 214);
        private static Color titlebarColor2 = Color.FromArgb(76, 123, 204);
        private static Color titlebarColor3 = Color.FromArgb(63, 111, 194);
        private static Color titlebarColor4 = Color.FromArgb(50, 99, 184);
        private static Color titlebarColor5 = Color.FromArgb(38, 88, 174);
        private static Color titlebarColor6 = Color.FromArgb(25, 76, 164);
        private static Color titlebarColor7 = Color.FromArgb(12, 64, 154);
        private static Color borderColor = Color.FromArgb(0, 0, 128);

        static StackRenderer()
        {
            titleBarGripBmp = StackView.DeserializeFromBase64Bmp(titleBarGripEnc);
        }

        public StackRenderer()
        {
        }

        public StackRenderer(ProfessionalColorTable professionalColorTable)
            : base(professionalColorTable)
        {
            titleBarGripBmp = StackView.DeserializeFromBase64Bmp(titleBarGripEnc);
        }


        private void DrawTitleBar(Graphics g, Rectangle rect)
        {
            //Uncomment if you want to draw the title bar

            /*
            // Assign the image for the grip.
            Image titlebarGrip = titleBarGripBmp;

            // Fill the titlebar. 
            // This produces the gradient and the rounded-corner effect.
            g.DrawLine(new Pen(titlebarColor1), rect.X, rect.Y, rect.X + rect.Width, rect.Y);
            g.DrawLine(new Pen(titlebarColor2), rect.X, rect.Y + 1, rect.X + rect.Width, rect.Y + 1);
            g.DrawLine(new Pen(titlebarColor3), rect.X, rect.Y + 2, rect.X + rect.Width, rect.Y + 2);
            g.DrawLine(new Pen(titlebarColor4), rect.X, rect.Y + 3, rect.X + rect.Width, rect.Y + 3);
            g.DrawLine(new Pen(titlebarColor5), rect.X, rect.Y + 4, rect.X + rect.Width, rect.Y + 4);
            g.DrawLine(new Pen(titlebarColor6), rect.X, rect.Y + 5, rect.X + rect.Width, rect.Y + 5);
            g.DrawLine(new Pen(titlebarColor7), rect.X, rect.Y + 6, rect.X + rect.Width, rect.Y + 6);

            // Center the titlebar grip.
            g.DrawImage(
                titlebarGrip,
                new Point(rect.X + ((rect.Width / 2) - (titlebarGrip.Width / 2)),
                rect.Y + 1));
             */ 
        }

        // This method handles the RenderGrip event.
        protected override void OnRenderGrip(ToolStripGripRenderEventArgs e)
        {
            DrawTitleBar(
                e.Graphics,
                new Rectangle(0, 0, e.ToolStrip.Width, 7));
        }

        // This method handles the RenderToolStripBorder event.
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            DrawTitleBar(
                e.Graphics,
                new Rectangle(0, 0, e.ToolStrip.Width, 7));
        }

        // This method handles the RenderButtonBackground event.
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle bounds = new Rectangle(Point.Empty, e.Item.Size);

            //Original (Windows XP style)
            //Color gradientBegin = Color.FromArgb(203, 225, 252);
            //Color gradientEnd = Color.FromArgb(125, 165, 224);
            Color gradientBegin = Color.FromArgb(221, 34, 29);
            Color gradientEnd = Color.FromArgb(221, 34, 29);

            ToolStripButton button = e.Item as ToolStripButton;
            if (button.Pressed || button.Checked)
            {
                //Original (Windows XP style)
                //gradientBegin = Color.FromArgb(254, 128, 62);
                //gradientEnd = Color.FromArgb(255, 223, 154);
                //MRM: Is not a state button, same color in both (pressed and unpressed)
                gradientBegin = Color.FromArgb(221, 34, 24);
                gradientEnd = Color.FromArgb(229, 85, 78);
            }
            else if (button.Selected)
            {
                gradientBegin = Color.FromArgb(255, 255, 222);
                gradientEnd = Color.FromArgb(255, 203, 136);
            }

            using (Brush b = new LinearGradientBrush(
                bounds,
                gradientBegin,
                gradientEnd,
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(b, bounds);
            }

            e.Graphics.DrawRectangle(
                SystemPens.ControlDarkDark,
                bounds);

            g.DrawLine(
                SystemPens.ControlDarkDark,
                bounds.X,
                bounds.Y,
                bounds.Width - 1,
                bounds.Y);

            g.DrawLine(
                SystemPens.ControlDarkDark,
                bounds.X,
                bounds.Y,
                bounds.X,
                bounds.Height - 1);

            ToolStrip toolStrip = button.Owner;
            ToolStripButton nextItem = button.Owner.GetItemAt(
                button.Bounds.X,
                button.Bounds.Bottom + 1) as ToolStripButton;

            if (nextItem == null)
            {
                g.DrawLine(
                    SystemPens.ControlDarkDark,
                    bounds.X,
                    bounds.Height - 1,
                    bounds.X + bounds.Width - 1,
                    bounds.Height - 1);
            }
        }
    }
}
