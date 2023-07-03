namespace mytest_cs;
public class MyUtils {

    public static int max_prime;
    public bool testing = true;
    public int[] primes;
    private static byte[] z = { 0, 0, 0, 2 };

    /// <summary>
    /// Used by crypt random.
    /// </summary>
    private static RNGCryptoServiceProvider rngCSP { get; set; } = new RNGCryptoServiceProvider();

    public MyUtils() { }
    public MyUtils(bool inTesting) {
        testing = inTesting;
        max_prime = Primes.GetPrimes().Last();
        primes = Primes.GetPrimes();
    }

    // ---------- Helper functions ----------- //
    public static int GetR(int x, int y) {
        if (x==y) return x;
        rngCSP.GetBytes(z);
        return (x--+(BitConverter.ToUInt16(z, 0)%(y-x))+1)-1;
    }
    public static double GetF() {
        var b = new byte[12];
        var rngCrypto = new RNGCryptoServiceProvider();
        rngCrypto.GetBytes(b);
        var stringBuilder = new StringBuilder("0.");
        var numbers = b.Select(i => Convert.ToInt32((i * 100 / 255)/10)).ToArray();

        foreach (var number in numbers) {
            stringBuilder.Append(number);
        }

        return Convert.ToDouble(stringBuilder.ToString());
    }

    public void myLog( String msg, [CallerMemberName] String name = "") {
        if (testing) {
            Console.WriteLine("\nError in " + name + ": " + msg);
        }
    }

    // ----------  Method caches ------------ //
    private Dictionary<int, bool> IsPrimeCache = new Dictionary<int, bool>();
    private Dictionary<int, int[]> primeFactorsCache = new Dictionary<int, int[]>();
    private Dictionary<int[], long> LcmCache = new Dictionary<int[], long>();
    private Dictionary<int[], int> HcfCache = new Dictionary<int[], int>();
    private Dictionary<(double x, int figures), double> SignificantFiguresCache = new Dictionary<(double x, int figures), double>();
    private Dictionary<double, string> StandardFormCache = new Dictionary<double, string>();

    // ----------  Utils functions ----------- //
    public int myTestRi(int x, int y, bool nonZero= true) {
        int rtn = 0;
        try {
            if (x == y) return x;
            double posBit = 0;
            double negBit = 0;
            if (x < 0 && y < 0) {
                x = Math.Abs(x);
                y = Math.Abs(y);
                if (x > y) 
                    (x,y) = (y,x);
                do {
                    rtn = GetR(x, y) * -1;
                } while (nonZero && rtn == 0);
            } else if (x < 0) {
                if (y == 0) {
                    do {
                        rtn = GetR(0, Math.Abs(x)) *-1;
                    } while (nonZero && rtn == 0);
                } else {
                    do {
                        posBit = rtn = GetR(0, Math.Abs(x)) *-1;
                        negBit = rtn = GetR(0, y);
                        rtn = (int)(posBit + negBit);
                    } while (nonZero && rtn == 0);
                }
            } else if (y < 0) {
                if (x == 0) {
                    do {
                        rtn = GetR(0, Math.Abs(y)) *-1;
                    } while (nonZero && rtn == 0);
                } else {
                    do {
                        posBit = GetR(0, Math.Abs(y)) *-1;
                        negBit = GetR(0, x) ;
                        rtn = (int)(posBit + negBit);
                    } while (nonZero && rtn == 0);
                }
            } else {
                if (x > y)
                    (x, y) = (y, x);
                int nudge = 0;
                if (x == 0)
                    nudge++;
                if (y == 0)
                    nudge++;
                do {
                    rtn =  GetR(x+nudge, y+nudge)-nudge;
                } while (nonZero && rtn == 0);
            }
        } catch (Exception ex) {
            myLog("myTestRi - " + ex.Message);
            throw;
        }
        return rtn;
    }

    private double GetDec(int z, double power) {
        double rtn=0.0;
        string n = "";
        do {
            n=(GetF()).ToString();
            Double.TryParse(n.Substring(0, 2+z), out rtn);
        } while (width(rtn) - 2 != z || Math.Round(rtn * power - 0.5, z) % 10 == 0 || rtn == 0.0);
        return rtn;
    }

    public double myTestRd(int x, int y, int z, bool nonZero=true) {
        double rtn = 0.0;
        try {
            if (z == 0) 
                return (double)myTestRi(x, y);
            z = Math.Abs(z);
            double power = Math.Pow(10.0, z);
            double dec = 0.0;
            dec = GetDec(z, power);

            if (x == y) {
                if (x < 0)
                    dec--;
                return ((x + dec) * power) / power;
            }
            int negBit = 0;
            int posBit = 0;
            if (x < 0 && y < 0) {
                x = Math.Abs(x);
                y = Math.Abs(y);
                if (x > y)
                    (x, y) = (y, x);
                if (y - x == 1) {
                    return (x + dec) * -1;
                } else {
                    do {
                        rtn = (myTestRi(x, (y - 1)) + dec) * -1;
                    } while (nonZero && rtn == 0);
                }
            } else if (x < 0) {
                if (y - x == 1) {
                    rtn = dec * -1;
                } else if (y == 0) {
                    rtn = (myTestRi(0, Math.Abs(x) - 1) + dec) * -1;
                } else {
                    do {
                        negBit = myTestRi(0, Math.Abs(x)) * -1;
                        posBit = myTestRi(0, y - 1);
                        rtn =((double)(negBit + posBit)) + (1.0 - dec);
                    } while (nonZero && rtn == 0.0);
                }
            } else if (y < 0) {
                if (x - y == 1) {
                    rtn = dec * -1;
                } else if (x == 0) {
                    rtn = (myTestRi(0, Math.Abs(y) - 1) + dec) * -1;
                } else {
                    do {
                        negBit = myTestRi(0, Math.Abs(y) - 1) * -1;
                        posBit = myTestRi(0, x - 1);
                        rtn = (double)(negBit + posBit) + (1 - dec);
                    } while (nonZero && rtn == 0.0);
                }
            } else {
                if (x > y)
                    (x, y) = (y, x);
                int nudge = 0;
                y--;
                if (x == 0)
                    nudge++;
                if (y == 0)
                    nudge++;
                do {
                    rtn = (myTestRi(x + nudge, y + nudge) - nudge) + dec;
                } while (nonZero && rtn == 0.0);
            }
        } catch (Exception ex) {
            myLog( "myTestRd - " + ex.Message);
            throw;
        }
        return rtn;
    }

    public int[] myTestRfr(int numWidth, int denomWidth, bool proper = true) {
        int num = 0;
        int denom = 0;
        try {
            if (numWidth <= 0 || denomWidth <= 0 || numWidth > 9 || denomWidth > 9)
                throw new Exception("Argument(s) exception: rfr(" + numWidth + ", " + denomWidth + "})");

            num = myTestRi((int)Math.Pow(10, numWidth - 1), (int)Math.Pow(10, numWidth) - 1);
            denom = myTestRi((int)Math.Pow(10, denomWidth - 1), (int)Math.Pow(10, denomWidth) - 1);
            if (numWidth > denomWidth) {
                proper = false;
            }
            while (num >= denom && proper) {
                num = myTestRi((int)Math.Pow(10, numWidth - 1), (int)Math.Pow(10, numWidth) - 1);
                denom = myTestRi((int)Math.Pow(10, denomWidth - 1), (int)Math.Pow(10, denomWidth) - 1);
            }
        } catch (Exception ex) {
            myLog( "myTestRfr - " + ex.Message);
            throw;
        }
        return new int[] { num, denom };
    }

    public bool myTestIsPrime(int x) {
        if(IsPrimeCache.ContainsKey(x)) 
            return IsPrimeCache[x];
        bool rtn = true;
        try {
            if (x == 2) {
                IsPrimeCache[x]=true;
                return true;
            }
            if (x < 2) {
                IsPrimeCache[x]=false;
                return false;
            }
            if (x % 2 == 0 && x > 2) {
                IsPrimeCache[x]=false;
                return false;
            }
            if (x < max_prime) {
                IsPrimeCache[x] = primes.Contains(x);
                return IsPrimeCache[x];
            } else {
                for (int y = 3; y <= x; y++) {
                    if (y % x == 0) {
                        IsPrimeCache[x]=false;
                        rtn= false;
                        break;
                    }
                }
            }
            IsPrimeCache[x]=rtn;
            return rtn;
        } catch (Exception ex) {
            myLog( "myTestIsPrime - " + ex.Message);
            throw;
        }
        return true; //uc
    }

    public int[] myTestPrimeFactors(int x) {
        if (primeFactorsCache.ContainsKey(x)) 
            return primeFactorsCache[x];
        List<int> rtn = new List<int>();
        int orig = x;
        try {
            //if (x > 1048577)
            if (x > 1000000)
                throw new Exception("Argument exception: myTestPrimefactors(" + x + "})");
            if (x < 2) {
                primeFactorsCache[x] =new int[] { 0 };
                return primeFactorsCache[x];
            }
            if (x < 4) {
                primeFactorsCache[x] =new int[] { x };
                return primeFactorsCache[x];
            }
            int i = 0;
            try {
                i = Math.Max(x / Math.Max(((Int32)(Math.Abs(x)).ToString().Length * 2), 1), 1)+5;
            } catch (Exception ex) {
                i = x;
            }
            int prime = 0;
            while (i >= 0) {
                prime = primes[i];
                if (x % prime == 0) {
                    rtn.Add( prime);
                    x /= prime;
                    try {
                        i = Math.Max(x / Math.Max(((Int32)(Math.Abs(x)).ToString().Length * 2), 1), 1)+5;
                    } catch (Exception ex) {
                        i = x;
                    }
                } else {
                    i -= 1;
                }
            }
        } catch (Exception ex) {
            myLog( "myTestPrimeFactors - " + ex.Message);
            throw;
        }
        rtn.Reverse();
        primeFactorsCache[orig] = rtn.ToArray();
        return primeFactorsCache[orig]; 
    }

    public long myTestLcm(int[] arr) {
        if(LcmCache.ContainsKey(arr))
            return LcmCache[arr];
        long rtn = 0;
        try {
            long mult = 1;
            arr=arr.Select(a=>Math.Abs(a)).ToArray();
            mult = arr.Aggregate(1, (a,b) => a*b);
            if (mult > 150000000)
                throw new Exception("\nInput array too large: " + mult + " - mytestLcm");
            rtn = mult;
            bool skip = false;
            int count = arr.Length;
            for (long i = mult; i > 0; i--) {
                skip = false;
                for (int j=0; j< count; j++) {
                    if (i % arr[j] != 0) {
                        skip = true;
                        break;
                    }
                }
                if (!skip) 
                    rtn = i;
            }
        } catch (Exception ex) {
            myLog( "myTestLcm - " + ex.Message);
            throw;
        }
        LcmCache[arr]=rtn;
        return rtn;
    }

    public int myTestHcf(int[] arr) {
        if (HcfCache.ContainsKey(arr))
            return HcfCache[arr];
        int rtn = 0;
        try {
            arr = arr.Select(a => Math.Abs(a)).ToArray();
            int bound = arr.Min();
            bool skip = false;
            int count = arr.Length;
            for (int i = 1; i <= bound; i++) {
                for (int j=0;j<count;j++) {
                    if (arr[j] % i != 0) {
                        skip = true;
                        break;
                    }
                }
                if (!skip) 
                    rtn = i;
                skip = false;
            }
        } catch (Exception ex) {
            myLog( "myTestHcf - " + ex.Message);
            throw;
        }
        HcfCache[arr]=rtn;
        return rtn;
    }

    public int width(int x) {
        return ((Int32)(Math.Abs(x))).ToString().Length;
    }

    public int width(double x) {
        return ((double)(Math.Abs(x))).ToString().Length;
    }

    public int myLibSquare(int x, int y) {
        int ran = 0;
        try {
            if (y < 0)
                throw new Exception("\nArgument(s) exception: " + x + ", " + y + " in myLibSquare");
            if (x == 0)
                return 0;
            ran = myTestRi(x, y);
        } catch (Exception ex) {
            myLog( "myLibSquare - " + ex.Message);
            throw;
        }
        return (int)Math.Pow(ran, 2);
    }

    public bool myLibIsSquare(int x) {
        bool rtn = false;
        try {
            if (x < 4)
                return false;
            rtn = myLibIsInt(Math.Sqrt((double)x));
        } catch (Exception ex) {
            myLog( "myLibIsSquare - " + ex.Message);
            throw;
        }
        return rtn;
    }

    public T myLibChoose<T>(T[] inSet) {
        return inSet[ GetR(0,inSet.Length-1) ];
    }

    public bool myLibIsInt(double x) {
        return x==Math.Ceiling(x);
    }

    public int myLibNotSquare(int x, int y) {
        try {
            int rtn = 4;
            while (myLibIsSquare(rtn)) {
                rtn=myTestRi(x, y);
                if (1==Math.Abs(rtn))
                    break;
            }
            return rtn;
        } catch (Exception ex) {
            myLog(ex.Message);
            throw;
        }
    }

    public bool myLibRunFuncUntilNot<T>(int[] notUs, Delegate func, int maxTries, params object[] args) where T: new(){
        T rslt = new T(); // TODO: Pass in a failure return
        try {
            for (int i = 0; i< maxTries; i++) {
                rslt = (T)func.DynamicInvoke(args);
                for(int n =0; n<notUs.Length; n++)
                    if (typeof(T).IsValueType ? !object.Equals(rslt, notUs[n]) : !ReferenceEquals(rslt, notUs[n]))
                        return true;
            }
            return false;
        } catch (Exception ex) {
            myLog( "myLibRunFuncUntilNot - " + ex.Message);
            throw;
        }
        return false;
    }

    public double myLibSignificantFigures(double x, int figures) {
        if(SignificantFiguresCache.ContainsKey((x, figures)))
            return SignificantFiguresCache[(x,figures)];
        double rtn = Math.Abs(x);
        try {
        if (figures < 1)
            throw new Exception("\nArgument(s) exception: " + x + ", " + figures + " in myLibSignificantFigures");
        if (x == 0)
            return 0;
        int shifts = 0;
        if (rtn < 1.0) {
            while (rtn < 1.0) {
                rtn *= 10;
                shifts--;
            }
        } else {
            while (rtn > 1.0) {
                rtn *= 0.1;
                shifts++;
            }
        }
        rtn *= Math.Pow(10, figures);
        rtn = Math.Round(rtn);
        rtn *= Math.Pow(10, shifts - figures);
        if (x < 0)
            rtn *= -1;
    } catch (Exception ex) {
        myLog( "myLibSignificantFigures - " + ex.Message);
            throw;
    }
    SignificantFiguresCache[(x, figures)]=rtn;
    return rtn;
}

    public String myLibToSup(String str) {
        StringBuilder sbRtn = new StringBuilder();
        try {
            foreach(char s in str) {
                switch (s) {
                    case '1':
                        sbRtn.Append("¹");
                        break;
                    case '2':
                        sbRtn.Append("²");
                        break;
                    case '3':
                        sbRtn.Append("³");
                        break;
                    case '4':
                        sbRtn.Append("⁴");
                        break;
                    case '5':
                        sbRtn.Append("⁵");
                        break;
                    case '6':
                        sbRtn.Append("⁶");
                        break;
                    case '7':
                        sbRtn.Append("⁷");
                        break;
                    case '8':
                        sbRtn.Append("⁸");
                        break;
                    case '9':
                        sbRtn.Append("⁹");
                        break;
                    case '0':
                        sbRtn.Append("⁰");
                        break;
                    case '-':
                        sbRtn.Append("⁻");
                        break;
                    case '+':
                        sbRtn.Append("⁺");
                        break;
                    case '*':
                        sbRtn.Append("*");
                        break;
                    case '/':
                        sbRtn.Append("ᐟ");
                        break;
                    case '\\':
                        sbRtn.Append("ᐠ");
                        break;
                    case 'a':
                        sbRtn.Append("ᵃ");
                        break;
                    case 'b':
                        sbRtn.Append("ᵇ");
                        break;
                    case 'c':
                        sbRtn.Append("ᶜ");
                        break;
                    case 'd':
                        sbRtn.Append("ᵈ");
                        break;
                    case 'e':
                        sbRtn.Append("ᵉ");
                        break;
                    case 'f':
                        sbRtn.Append("ᶠ");
                        break;
                    case 'g':
                        sbRtn.Append("ᵍ");
                        break;
                    case 'h':
                        sbRtn.Append("ʰ");
                        break;
                    case 'i':
                        sbRtn.Append("ⁱ");
                        break;
                    case 'j':
                        sbRtn.Append("ʲ");
                        break;
                    case 'k':
                        sbRtn.Append("ᵏ");
                        break;
                    case 'l':
                        sbRtn.Append("ˡ");
                        break;
                    case 'm':
                        sbRtn.Append("ᵐ");
                        break;
                    case 'n':
                        sbRtn.Append("ⁿ");
                        break;
                    case 'o':
                        sbRtn.Append("ᵒ");
                        break;
                    case 'p':
                        sbRtn.Append("ᵖ");
                        break;
                    case 'q':
                        sbRtn.Append("۹");
                        break;
                    case 'r':
                        sbRtn.Append("ʳ");
                        break;
                    case 's':
                        sbRtn.Append("ˢ");
                        break;
                    case 't':
                        sbRtn.Append("ᵗ");
                        break;
                    case 'u':
                        sbRtn.Append("ᵘ");
                        break;
                    case 'v':
                        sbRtn.Append("ᵛ");
                        break;
                    case 'w':
                        sbRtn.Append("ʷ");
                        break;
                    case 'x':
                        sbRtn.Append("ˣ");
                        break;
                    case 'y':
                        sbRtn.Append("ʸ");
                        break;
                    case 'z':
                        sbRtn.Append("ᶻ");
                        break;
                    case 'A':
                        sbRtn.Append("ᴬ");
                        break;
                    case 'B':
                        sbRtn.Append("ᴮ");
                        break;
                    case 'C':
                        sbRtn.Append("ᶜ");
                        break;
                    case 'D':
                        sbRtn.Append("ᴰ");
                        break;
                    case 'E':
                        sbRtn.Append("ᴱ");
                        break;
                    case 'F':
                        sbRtn.Append("ᶠ");
                        break;
                    case 'G':
                        sbRtn.Append("ᴳ");
                        break;
                    case 'H':
                        sbRtn.Append("ᴴ");
                        break;
                    case 'I':
                        sbRtn.Append("ᴵ");
                        break;
                    case 'J':
                        sbRtn.Append("ᴶ");
                        break;
                    case 'K':
                        sbRtn.Append("ᴷ");
                        break;
                    case 'L':
                        sbRtn.Append("ᴸ");
                        break;
                    case 'M':
                        sbRtn.Append("ᴹ");
                        break;
                    case 'N':
                        sbRtn.Append("ᴺ");
                        break;
                    case 'O':
                        sbRtn.Append("ᴼ");
                        break;
                    case 'P':
                        sbRtn.Append("ᴾ");
                        break;
                    case 'R':
                        sbRtn.Append("ᴿ");
                        break;
                    case 'T':
                        sbRtn.Append("ᵀ");
                        break;
                    case 'S':
                        sbRtn.Append("ˢ");
                        break;
                    case 'U':
                        sbRtn.Append("ᵁ");
                        break;
                    case 'V':
                        sbRtn.Append("ⱽ");
                        break;
                    case 'W':
                        sbRtn.Append("ᵂ");
                        break;
                    case 'X':
                        sbRtn.Append("ˣ");
                        break;
                    case 'Y':
                        sbRtn.Append("ʸ");
                        break;
                    case 'Z':
                        sbRtn.Append("ᶻ");
                        break;
                    case '=':
                        sbRtn.Append("⁼");
                        break;
                    case '(':
                        sbRtn.Append("⁽");
                        break;
                    case ')':
                        sbRtn.Append("⁾");
                        break;
                    default:
                        sbRtn.Append(s);
                            break;
                }
            }
        } catch (Exception ex) {
            myLog( "myLibToSuper - " + ex.Message);
            throw;
        }
        return sbRtn.ToString();
    }

    public String myLibToSub(String str) {
        StringBuilder sbRtn = new StringBuilder();
        try {
            foreach(char s in str) {
                switch (s) {
                    case '1':
                        sbRtn.Append("₁");
                        break;
                    case '2':
                        sbRtn.Append("₂");
                        break;
                    case '3':
                        sbRtn.Append("₃");
                        break;
                    case '4':
                        sbRtn.Append("₄");
                        break;
                    case '5':
                        sbRtn.Append("₅");
                        break;
                    case '6':
                        sbRtn.Append("₆");
                        break;
                    case '7':
                        sbRtn.Append("₇");
                        break;
                    case '8':
                        sbRtn.Append("₈");
                        break;
                    case '9':
                        sbRtn.Append("₉");
                        break;
                    case '0':
                        sbRtn.Append("₀");
                        break;
                    case '+':
                        sbRtn.Append("₊");
                        break;
                    case '-':
                        sbRtn.Append("₋");
                        break;
                    case '=':
                        sbRtn.Append("₌");
                        break;
                    case '(':
                        sbRtn.Append("₍");
                        break;
                    case ')':
                        sbRtn.Append("₎");
                        break;
                    case 'a':
                        sbRtn.Append("ₐ");
                        break;
                    case 'b':
                        sbRtn.Append("♭");
                        break;
                    case 'e':
                        sbRtn.Append("ₑ");
                        break;
                    case 'g':
                        sbRtn.Append("₉");
                        break;
                    case 'h':
                        sbRtn.Append("ₕ");
                        break;
                    case 'i':
                        sbRtn.Append("ᵢ");
                        break;
                    case 'j':
                        sbRtn.Append("ⱼ");
                        break;
                    case 'k':
                        sbRtn.Append("ₖ");
                        break;
                    case 'l':
                        sbRtn.Append("ₗ");
                        break;
                    case 'm':
                        sbRtn.Append("ₘ");
                        break;
                    case 'n':
                        sbRtn.Append("ₙ");
                        break;
                    case 'o':
                        sbRtn.Append("ₒ");
                        break;
                    case 'p':
                        sbRtn.Append("ₚ");
                        break;
                    case 'r':
                        sbRtn.Append("ᵣ");
                        break;
                    case 's':
                        sbRtn.Append("ₛ");
                        break;
                    case 't':
                        sbRtn.Append("ₜ");
                        break;
                    case 'u':
                        sbRtn.Append("ᵤ");
                        break;
                    case 'v':
                        sbRtn.Append("ᵥ");
                        break;
                    case 'x':
                        sbRtn.Append("ₓ");
                        break;
                    case 'A':
                        sbRtn.Append("ₐ");
                        break;
                    case 'B':
                        sbRtn.Append("₈");
                        break;
                    case 'E':
                        sbRtn.Append("ₑ");
                        break;
                    case 'F':
                        sbRtn.Append("բ");
                        break;
                    case 'G':
                        sbRtn.Append("₉");
                        break;
                    case 'H':
                        sbRtn.Append("ₕ");
                        break;
                    case 'I':
                        sbRtn.Append("ᵢ");
                        break;
                    case 'J':
                        sbRtn.Append("ⱼ");
                        break;
                    case 'K':
                        sbRtn.Append("ₖ");
                        break;
                    case 'L':
                        sbRtn.Append("ₗ");
                        break;
                    case 'M':
                        sbRtn.Append("ₘ");
                        break;
                    case 'N':
                        sbRtn.Append("ₙ");
                        break;
                    case 'O':
                        sbRtn.Append("ₒ");
                        break;
                    case 'P':
                        sbRtn.Append("ₚ");
                        break;
                    case 'R':
                        sbRtn.Append("ᵣ");
                        break;
                    case 'T':
                        sbRtn.Append("ₜ");
                        break;
                    case 'S':
                        sbRtn.Append("ₛ");
                        break;
                    case 'U':
                        sbRtn.Append("ᵤ");
                        break;
                    case 'V':
                        sbRtn.Append("ᵥ");
                        break;
                    case 'W':
                        sbRtn.Append("w");
                        break;
                    case 'X':
                        sbRtn.Append("ₓ");
                        break;
                    case 'Y':
                        sbRtn.Append("ᵧ");
                        break;
                    case 'Z':
                        sbRtn.Append("Z");
                        break;
                    default:
                        sbRtn.Append(s);
                        break;
                    }
                }
            } catch (Exception ex) {
                myLog( "myLibToSub - " + ex.Message);
            throw;
            }
            return sbRtn.ToString();
        }

    public String myLibToStandardForm(double x) {
        if (StandardFormCache.ContainsKey(x))
            return StandardFormCache[x];
        int sign = 0;
        double orig = x;
        bool expSigned = false;
        int shifts = 0;
        int floatLen = 0;
        try {
            if (0 == x)
                return "0";
            if (1 == x)
                return "1";
            if (-1 == x)
                return "-1";
            sign = x < 0 ? -1 : 1;
            expSigned = Math.Abs(x) < 1 ? false : true;
            String[] splitDP;
            int EShift = 0;
            if (x.ToString().Contains('E')) {
                //1.2345E-4
                String[] SplitOnE = x.ToString().Split('E', 2); // [1.2345], [-4]
                Int32.TryParse(SplitOnE[1], out EShift); //-4
                EShift *=-1;
                String minusE = SplitOnE[0];            // 1.2345
                splitDP = minusE.Split('.', 2);
                if (sign==-1)
                    floatLen--;
            } else {
                splitDP =x.ToString().Split('.', 2);
            }
            floatLen += splitDP[splitDP.Length - 1].Length+EShift;
            int i = 0;
            if (myLibIsInt(x))
                floatLen=0;
            if (Math.Abs(x) >= 1.0 && Math.Abs(x) < 10.0)
                return x.ToString();
            x=Math.Abs(x);
            if (x<1) {
                while (x<1.0) {
                    x*=10.0;
                    shifts++;
                    floatLen--;
                }
            } else {
                while (x>=10.0) {
                    x/=10.0;
                    shifts++;
                    floatLen++;
                }
            }
        } catch (Exception ex) {
            myLog( "myLibToStandardForm - " + ex.Message);
            throw;
        }

        //return (x*sign).ToString("F"+floatLen.ToString()) + "*10" + myLibToSup((expSigned ? "-" : "") + shifts.ToString());
        StandardFormCache[orig] = (x*sign).ToString("F"+floatLen.ToString()) + "*10" + myLibToSup((expSigned ? "-" : "") + shifts.ToString());
        return StandardFormCache[orig];
    }
}