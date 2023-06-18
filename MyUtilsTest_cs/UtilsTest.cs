using System;

namespace MyUtilsTest_cs;

[TestClass]
public class UtilsTest {
    MyUtils utils = new MyUtils(true);

    // ------ Helper functions ------ //

    private bool test_ri_help(int x, int y) {
        var rslt = utils.myTestRi(x, y);
        return (Math.Min(x, y)<=rslt) && (rslt<=Math.Max(x, y));
    }
    private bool test_rfr_help(int num, int denom) {
        var fr = utils.myTestRfr(num, denom);
        var numL= Math.Floor(Math.Log(fr[0],10))+1;
        var denL = Math.Floor(Math.Log(fr[1], 10))+1;
        return numL==num && denL==denom;
    }
    private bool test_rd_help(int x, int y, int z) {
        bool int_good = false;
        bool dec_good = false;
        double rslt = utils.myTestRd(x, y, z);
        int origx = x;
        int origy = y;
        //x=Math.Abs(x);
        //y=Math.Abs(y);
        (x, y) = (Math.Min(x, y), Math.Max(x, y));
        if (x==y) {
            if (y<0) {
                if (x<0) {
                    int_good = x-1 <=rslt && rslt <= y+1;
                }else{
                    int_good = x <=rslt && rslt <= y-1;
                }
            } else {
                if (x<0) {
                    int_good = x-1 <=rslt && rslt <= y+1;
                } else {
                    int_good = x <=rslt && rslt <= y+1;
                }
            }
        } else {
            int_good = x <=rslt && rslt <= y;
        }
        if (z==0) {
            dec_good=true;
        } else {
            z=Math.Abs(z);
            dec_good = Math.Round(Math.Abs(rslt)-(int)Math.Abs(rslt), z).ToString().Length-2 == z;
        }
        return int_good && dec_good;
    }
    private bool notFixated<T>(int[] notUs, Delegate func, params object[] args) where T: new() {
        return utils.myLibRunFuncUntilNot<T>(notUs, func, 250, args);
    }
    private bool notBookendBiased<T>(Delegate func, params object[] args) where T : new(){
        int x = (int)args[0];
        int y = (int)args[1];
        if(Math.Abs(x-y)>1 && Math.Abs(y-x)>1)
            return utils.myLibRunFuncUntilNot<T>(new int[] {x,y }, func, 250, args);
        return true;
    }

    // -------  Tests ------- //

    [TestMethod]
    public void test_ri() {
        Assert.IsTrue(notBookendBiased<int>(utils.myTestRi, 1, 1, true));
        Assert.IsTrue(notBookendBiased<int>(utils.myTestRi, 5, 10, true));
        Assert.IsTrue(test_ri_help(1, 1));
        Assert.IsTrue(test_ri_help(-1, 1));
        Assert.IsTrue(test_ri_help(1, -1));
        Assert.IsTrue(test_ri_help(-1, -1));
        Assert.IsTrue(test_ri_help(0, 0));
        Assert.IsTrue(test_ri_help(1, 0));
        Assert.IsTrue(test_ri_help(-1, 0));
        Assert.IsTrue(test_ri_help(0, 1));
        Assert.IsTrue(test_ri_help(0, -1));
        Assert.IsTrue(test_ri_help(5, 5));
        Assert.IsTrue(test_ri_help(-5, 5));
        Assert.IsTrue(test_ri_help(5, -5));
        Assert.IsTrue(test_ri_help(-5, -5));
        Assert.IsTrue(test_ri_help(5, 0));
        Assert.IsTrue(test_ri_help(-5, 0));
        Assert.IsTrue(test_ri_help(0, 5));
        Assert.IsTrue(test_ri_help(0, -5));
        Assert.IsTrue(test_ri_help(6, 3));
        Assert.IsTrue(test_ri_help(-6, 3));
        Assert.IsTrue(test_ri_help(6, -3));
        Assert.IsTrue(test_ri_help(-6, -3));
        Assert.IsTrue(test_ri_help(3, 6));
        Assert.IsTrue(test_ri_help(-3, 6));
        Assert.IsTrue(test_ri_help(3, -6));
        Assert.IsTrue(test_ri_help(-3, -6));
        Assert.IsTrue(test_ri_help(10, 1));
        Assert.IsTrue(test_ri_help(100, 10));
        Assert.IsTrue(test_ri_help(1000, 100));
        Assert.IsTrue(test_ri_help(10000, 1000));
        Assert.IsTrue(test_ri_help(100000, 10000));
        Assert.IsTrue(test_ri_help(1000000, 100000));
        Assert.IsTrue(test_ri_help(-10, 10));
        Assert.IsTrue(test_ri_help(-100, 100));
        Assert.IsTrue(test_ri_help(-1000, 1000));
        Assert.IsTrue(test_ri_help(-10000, 10000));
        Assert.IsTrue(test_ri_help(-100000, 100000));
        Assert.IsTrue(test_ri_help(-1000000, 1000000));
        Assert.IsTrue(test_ri_help(10, -10));
        Assert.IsTrue(test_ri_help(100, -100));
        Assert.IsTrue(test_ri_help(1000, -1000));
        Assert.IsTrue(test_ri_help(10000, -10000));
        Assert.IsTrue(test_ri_help(100000, -100000));
        Assert.IsTrue(test_ri_help(1000000, -1000000));
        Assert.IsTrue(test_ri_help(-1, -10));
        Assert.IsTrue(test_ri_help(-10, -100));
        Assert.IsTrue(test_ri_help(-100, -1000));
        Assert.IsTrue(test_ri_help(-1000, -10000));
        Assert.IsTrue(test_ri_help(-10000, -100000));
        Assert.IsTrue(test_ri_help(-100000, -1000000));
        Assert.IsTrue(test_ri_help(-50000000, 1));
        Assert.IsTrue(test_ri_help(1, 50000000));
        Assert.IsTrue(test_ri_help(-50000000, 50000000));
    }

    [TestMethod]
    public void test_rfr() {
        Assert.IsTrue(test_rfr_help(2, 2));
        Assert.IsTrue(test_rfr_help(1, 1));
        Assert.IsTrue(test_rfr_help(1, 1));
        Assert.IsTrue(test_rfr_help(1, 2));
        Assert.IsTrue(test_rfr_help(1, 3));
        Assert.IsTrue(test_rfr_help(1, 4));
        Assert.IsTrue(test_rfr_help(1, 5));
        Assert.IsTrue(test_rfr_help(2, 1));
        Assert.IsTrue(test_rfr_help(3, 1));
        Assert.IsTrue(test_rfr_help(4, 1));
        Assert.IsTrue(test_rfr_help(5, 1));
        Assert.IsTrue(test_rfr_help(2, 3));
        Assert.IsTrue(test_rfr_help(2, 4));
        Assert.IsTrue(test_rfr_help(2, 5));
        Assert.IsTrue(test_rfr_help(2, 6));
        Assert.IsTrue(test_rfr_help(3, 2));
        Assert.IsTrue(test_rfr_help(4, 2));
        Assert.IsTrue(test_rfr_help(5, 2));
        Assert.IsTrue(test_rfr_help(6, 2));
        Assert.IsTrue(test_rfr_help(5, 6));
        Assert.IsTrue(test_rfr_help(5, 7));
        Assert.IsTrue(test_rfr_help(5, 8));
        Assert.IsTrue(test_rfr_help(5, 9));
        Assert.IsTrue(test_rfr_help(6, 5));
        Assert.IsTrue(test_rfr_help(7, 5));
        Assert.IsTrue(test_rfr_help(8, 5));
        Assert.IsTrue(test_rfr_help(9, 5));
    }

    [TestMethod]
    public void test_rd() {
        Assert.IsTrue(notBookendBiased<double>(utils.myTestRd, 1, 1, 1, true));
        Assert.IsTrue(notBookendBiased<double>(utils.myTestRd, 5,10, 3, true));
        Assert.IsTrue(test_rd_help(1, 1, 0));
        Assert.IsTrue(test_rd_help(-1, 1, 0));
        Assert.IsTrue(test_rd_help(1, -1, 0));
        Assert.IsTrue(test_rd_help(-1, -1, 0));
        Assert.IsTrue(test_rd_help(0, 0, 0));
        Assert.IsTrue(test_rd_help(1, 0, 0));
        Assert.IsTrue(test_rd_help(-1, 0, 0));
        Assert.IsTrue(test_rd_help(0, 1, 0));
        Assert.IsTrue(test_rd_help(0, -1, 0));
        Assert.IsTrue(test_rd_help(5, 5, 0));
        Assert.IsTrue(test_rd_help(-5, 5, 0));
        Assert.IsTrue(test_rd_help(5, -5, 0));
        Assert.IsTrue(test_rd_help(-5, -5, 0));
        Assert.IsTrue(test_rd_help(5, 0, 0));
        Assert.IsTrue(test_rd_help(-5, 0, 0));
        Assert.IsTrue(test_rd_help(0, 5, 0));
        Assert.IsTrue(test_rd_help(0, -5, 0));
        Assert.IsTrue(test_rd_help(6, 3, 0));
        Assert.IsTrue(test_rd_help(-6, 3, 0));
        Assert.IsTrue(test_rd_help(6, -3, 0));
        Assert.IsTrue(test_rd_help(-6, -3, 0));
        Assert.IsTrue(test_rd_help(3, 6, 0));
        Assert.IsTrue(test_rd_help(-3, 6, 0));
        Assert.IsTrue(test_rd_help(3, -6, 0));
        Assert.IsTrue(test_rd_help(-3, -6, 0));
        Assert.IsTrue(test_rd_help(1, 1, 1));
        Assert.IsTrue(test_rd_help(-1, 1, 1));
        Assert.IsTrue(test_rd_help(1, -1, 1));
        Assert.IsTrue(test_rd_help(-1, -1, 1));
        Assert.IsTrue(test_rd_help(0, 0, 1));
        Assert.IsTrue(test_rd_help(1, 0, 1));
        Assert.IsTrue(test_rd_help(-1, 0, 1));
        Assert.IsTrue(test_rd_help(0, 1, 1));
        Assert.IsTrue(test_rd_help(0, -1, 1));
        Assert.IsTrue(test_rd_help(5, 5, 1));
        Assert.IsTrue(test_rd_help(-5, 5, 1));
        Assert.IsTrue(test_rd_help(5, -5, 1));
        Assert.IsTrue(test_rd_help(-5, -5, 1));
        Assert.IsTrue(test_rd_help(5, 0, 1));
        Assert.IsTrue(test_rd_help(-5, 0, 1));
        Assert.IsTrue(test_rd_help(0, 5, 1));
        Assert.IsTrue(test_rd_help(0, -5, 1));
        Assert.IsTrue(test_rd_help(6, 3, 1));
        Assert.IsTrue(test_rd_help(-6, 3, 1));
        Assert.IsTrue(test_rd_help(6, -3, 1));
        Assert.IsTrue(test_rd_help(-6, -3, 1));
        Assert.IsTrue(test_rd_help(3, 6, 1));
        Assert.IsTrue(test_rd_help(-3, 6, 1));
        Assert.IsTrue(test_rd_help(3, -6, 1));
        Assert.IsTrue(test_rd_help(-3, -6, 1));
        Assert.IsTrue(test_rd_help(1, 1, 4));
        Assert.IsTrue(test_rd_help(-1, 1, 4));
        Assert.IsTrue(test_rd_help(1, -1, 4));
        Assert.IsTrue(test_rd_help(-1, -1, 4));
        Assert.IsTrue(test_rd_help(0, 0, 4));
        Assert.IsTrue(test_rd_help(1, 0, 4));
        Assert.IsTrue(test_rd_help(-1, 0, 4));
        Assert.IsTrue(test_rd_help(0, 1, 4));
        Assert.IsTrue(test_rd_help(0, -1, 4));
        Assert.IsTrue(test_rd_help(5, 5, 4));
        Assert.IsTrue(test_rd_help(-5, 5, 4));
        Assert.IsTrue(test_rd_help(5, -5, 4));
        Assert.IsTrue(test_rd_help(-5, -5, 4));
        Assert.IsTrue(test_rd_help(5, 0, 4));
        Assert.IsTrue(test_rd_help(-5, 0, 4));
        Assert.IsTrue(test_rd_help(0, 5, 4));
        Assert.IsTrue(test_rd_help(0, -5, 4));
        Assert.IsTrue(test_rd_help(6, 3, 4));
        Assert.IsTrue(test_rd_help(-6, 3, 4));
        Assert.IsTrue(test_rd_help(6, -3, 4));
        Assert.IsTrue(test_rd_help(-6, -3, 4));
        Assert.IsTrue(test_rd_help(3, 6, 4));
        Assert.IsTrue(test_rd_help(-3, 6, 4));
        Assert.IsTrue(test_rd_help(3, -6, 4));
        Assert.IsTrue(test_rd_help(-3, -6, 4));
        Assert.IsTrue(test_rd_help(1, 1, -4));
        Assert.IsTrue(test_rd_help(-1, 1, -4));
        Assert.IsTrue(test_rd_help(1, -1, -4));
        Assert.IsTrue(test_rd_help(-1, -1, -4));
        Assert.IsTrue(test_rd_help(0, 0, -4));
        Assert.IsTrue(test_rd_help(1, 0, -4));
        Assert.IsTrue(test_rd_help(-1, 0, -4));
        Assert.IsTrue(test_rd_help(0, 1, -4));
        Assert.IsTrue(test_rd_help(0, -1, -4));
        Assert.IsTrue(test_rd_help(5, 5, -4));
        Assert.IsTrue(test_rd_help(-5, 5, -4));
        Assert.IsTrue(test_rd_help(5, -5, -4));
        Assert.IsTrue(test_rd_help(-5, -5, -4));
        Assert.IsTrue(test_rd_help(5, 0, -4));
        Assert.IsTrue(test_rd_help(-5, 0, -4));
        Assert.IsTrue(test_rd_help(0, 5, -4));
        Assert.IsTrue(test_rd_help(0, -5, -4));
        Assert.IsTrue(test_rd_help(6, 3, -4));
        Assert.IsTrue(test_rd_help(-6, 3, -4));
        Assert.IsTrue(test_rd_help(6, -3, -4));
        Assert.IsTrue(test_rd_help(-6, -3, -4));
        Assert.IsTrue(test_rd_help(3, 6, -4));
        Assert.IsTrue(test_rd_help(-3, 6, -4));
        Assert.IsTrue(test_rd_help(3, -6, -4));
        Assert.IsTrue(test_rd_help(-3, -6, -4));
        Assert.IsTrue(test_rd_help(10, 1, 0));
        Assert.IsTrue(test_rd_help(100, 10, 0));
        Assert.IsTrue(test_rd_help(1000, 100, 0));
        Assert.IsTrue(test_rd_help(10000, 1000, 0));
        Assert.IsTrue(test_rd_help(100000, 10000, 0));
        Assert.IsTrue(test_rd_help(1000000, 100000, 0));
        Assert.IsTrue(test_rd_help(-10, 10, 0));
        Assert.IsTrue(test_rd_help(-100, 100, 0));
        Assert.IsTrue(test_rd_help(-1000, 1000, 0));
        Assert.IsTrue(test_rd_help(-10000, 10000, 0));
        Assert.IsTrue(test_rd_help(-100000, 100000, 0));
        Assert.IsTrue(test_rd_help(-1000000, 1000000, 0));
        Assert.IsTrue(test_rd_help(10, -10, 0));
        Assert.IsTrue(test_rd_help(100, -100, 0));
        Assert.IsTrue(test_rd_help(1000, -1000, 0));
        Assert.IsTrue(test_rd_help(10000, -10000, 0));
        Assert.IsTrue(test_rd_help(100000, -100000, 0));
        Assert.IsTrue(test_rd_help(1000000, -1000000, 0));
        Assert.IsTrue(test_rd_help(-1, -10, 0));
        Assert.IsTrue(test_rd_help(-10, -100, 0));
        Assert.IsTrue(test_rd_help(-100, -1000, 0));
        Assert.IsTrue(test_rd_help(-1000, -10000, 0));
        Assert.IsTrue(test_rd_help(-10000, -100000, 0));
        Assert.IsTrue(test_rd_help(-100000, -1000000, 0));
        Assert.IsTrue(test_rd_help(10, 1, 1));
        Assert.IsTrue(test_rd_help(100, 10, 1));
        Assert.IsTrue(test_rd_help(1000, 100, 1));
        Assert.IsTrue(test_rd_help(10000, 1000, 1));
        Assert.IsTrue(test_rd_help(100000, 10000, 1));
        Assert.IsTrue(test_rd_help(1000000, 100000, 1));
        Assert.IsTrue(test_rd_help(-10, 10, 1));
        Assert.IsTrue(test_rd_help(-100, 100, 1));
        Assert.IsTrue(test_rd_help(-1000, 1000, 1));
        Assert.IsTrue(test_rd_help(-10000, 10000, 1));
        Assert.IsTrue(test_rd_help(-100000, 100000, 1));
        Assert.IsTrue(test_rd_help(-1000000, 1000000, 1));
        Assert.IsTrue(test_rd_help(10, -10, 1));
        Assert.IsTrue(test_rd_help(100, -100, 1));
        Assert.IsTrue(test_rd_help(1000, -1000, 1));
        Assert.IsTrue(test_rd_help(10000, -10000, 1));
        Assert.IsTrue(test_rd_help(100000, -100000, 1));
        Assert.IsTrue(test_rd_help(1000000, -1000000, 1));
        Assert.IsTrue(test_rd_help(-1, -10, 1));
        Assert.IsTrue(test_rd_help(-10, -100, 1));
        Assert.IsTrue(test_rd_help(-100, -1000, 1));
        Assert.IsTrue(test_rd_help(-1000, -10000, 1));
        Assert.IsTrue(test_rd_help(-10000, -100000, 1));
        Assert.IsTrue(test_rd_help(-100000, -1000000, 1));
        Assert.IsTrue(test_rd_help(10, 1, 4));
        Assert.IsTrue(test_rd_help(100, 10, 4));
        Assert.IsTrue(test_rd_help(1000, 100, 4));
        Assert.IsTrue(test_rd_help(10000, 1000, 4));
        Assert.IsTrue(test_rd_help(100000, 10000, 4));
        Assert.IsTrue(test_rd_help(1000000, 100000, 4));
        Assert.IsTrue(test_rd_help(-10, 10, 4));
        Assert.IsTrue(test_rd_help(-100, 100, 4));
        Assert.IsTrue(test_rd_help(-1000, 1000, 4));
        Assert.IsTrue(test_rd_help(-10000, 10000, 4));
        Assert.IsTrue(test_rd_help(-100000, 100000, 4));
        Assert.IsTrue(test_rd_help(-1000000, 1000000, 4));
        Assert.IsTrue(test_rd_help(10, -10, 4));
        Assert.IsTrue(test_rd_help(100, -100, 4));
        Assert.IsTrue(test_rd_help(1000, -1000, 4));
        Assert.IsTrue(test_rd_help(10000, -10000, 4));
        Assert.IsTrue(test_rd_help(100000, -100000, 4));
        Assert.IsTrue(test_rd_help(1000000, -1000000, 4));
        Assert.IsTrue(test_rd_help(-1, -10, 4));
        Assert.IsTrue(test_rd_help(-10, -100, 4));
        Assert.IsTrue(test_rd_help(-100, -1000, 4));
        Assert.IsTrue(test_rd_help(-1000, -10000, 4));
        Assert.IsTrue(test_rd_help(-10000, -100000, 4));
        Assert.IsTrue(test_rd_help(-100000, -1000000, 4));
    }

    [TestMethod]
    public void test_is_prime() {
        Assert.IsFalse(utils.myTestIsPrime(-1));
        Assert.IsFalse(utils.myTestIsPrime(0));
        Assert.IsFalse(utils.myTestIsPrime(1));
        Assert.IsTrue(utils.myTestIsPrime(2));
        Assert.IsTrue(utils.myTestIsPrime(3));
        Assert.IsFalse(utils.myTestIsPrime(4));
        Assert.IsTrue(utils.myTestIsPrime(5));
        Assert.IsFalse(utils.myTestIsPrime(6));
        Assert.IsTrue(utils.myTestIsPrime(7));
        Assert.IsFalse(utils.myTestIsPrime(8));
        Assert.IsFalse(utils.myTestIsPrime(9));
        Assert.IsFalse(utils.myTestIsPrime(10));
        Assert.IsTrue(utils.myTestIsPrime(11));
        Assert.IsFalse(utils.myTestIsPrime(12));
        Assert.IsTrue(utils.myTestIsPrime(13));
        Assert.IsFalse(utils.myTestIsPrime(14));
        Assert.IsFalse(utils.myTestIsPrime(15));
        Assert.IsFalse(utils.myTestIsPrime(16));
        Assert.IsTrue(utils.myTestIsPrime(17));
        Assert.IsFalse(utils.myTestIsPrime(18));
        Assert.IsTrue(utils.myTestIsPrime(19));
        Assert.IsFalse(utils.myTestIsPrime(20));
        Assert.IsFalse(utils.myTestIsPrime(21));
        Assert.IsFalse(utils.myTestIsPrime(22));
        Assert.IsTrue(utils.myTestIsPrime(23));
        Assert.IsTrue(utils.myTestIsPrime(31));
        Assert.IsFalse(utils.myTestIsPrime(33));
        Assert.IsTrue(utils.myTestIsPrime(37));
        Assert.IsTrue(utils.myTestIsPrime(41));
        Assert.IsTrue(utils.myTestIsPrime(43));
        Assert.IsTrue(utils.myTestIsPrime(47));
        Assert.IsFalse(utils.myTestIsPrime(51));
        Assert.IsTrue(utils.myTestIsPrime(53));
        Assert.IsFalse(utils.myTestIsPrime(57));
        Assert.IsTrue(utils.myTestIsPrime(61));
        Assert.IsFalse(utils.myTestIsPrime(63));
        Assert.IsTrue(utils.myTestIsPrime(67));
        Assert.IsTrue(utils.myTestIsPrime(71));
        Assert.IsTrue(utils.myTestIsPrime(73));
        Assert.IsFalse(utils.myTestIsPrime(77));
        Assert.IsFalse(utils.myTestIsPrime(81));
        Assert.IsTrue(utils.myTestIsPrime(83));
        Assert.IsFalse(utils.myTestIsPrime(87));
        Assert.IsFalse(utils.myTestIsPrime(91));
        Assert.IsFalse(utils.myTestIsPrime(93));
        Assert.IsTrue(utils.myTestIsPrime(97));
        Assert.IsTrue(utils.myTestIsPrime(101));
        Assert.IsTrue(utils.myTestIsPrime(103));
        Assert.IsTrue(utils.myTestIsPrime(107));
        Assert.IsTrue(utils.myTestIsPrime(151));
        Assert.IsFalse(utils.myTestIsPrime(153));
        Assert.IsTrue(utils.myTestIsPrime(157));
        Assert.IsFalse(utils.myTestIsPrime(501));
        Assert.IsTrue(utils.myTestIsPrime(503));
        Assert.IsFalse(utils.myTestIsPrime(507));
        Assert.IsFalse(utils.myTestIsPrime(1001));
        Assert.IsFalse(utils.myTestIsPrime(1003));
        Assert.IsFalse(utils.myTestIsPrime(1007));
        Assert.IsFalse(utils.myTestIsPrime(10001));
        Assert.IsFalse(utils.myTestIsPrime(10003));
        Assert.IsTrue(utils.myTestIsPrime(10007));
        Assert.IsFalse(utils.myTestIsPrime(100001));
        Assert.IsTrue(utils.myTestIsPrime(100003));
        Assert.IsFalse(utils.myTestIsPrime(100007));
        Assert.IsFalse(utils.myTestIsPrime(1000001));
        Assert.IsTrue(utils.myTestIsPrime(1000003));
        Assert.IsFalse(utils.myTestIsPrime(1000007));
    }

    [TestMethod]
    public void test_prime_factors() {
        Assert.IsTrue(utils.myTestPrimeFactors(-1).SequenceEqual(new int[] { 0 }));
        Assert.IsTrue(utils.myTestPrimeFactors(0).SequenceEqual(new int[] { 0 }));
        Assert.IsTrue(utils.myTestPrimeFactors(1).SequenceEqual(new int[] { 0 }));
        Assert.IsTrue(utils.myTestPrimeFactors(2).SequenceEqual(new int[] { 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(3).SequenceEqual(new int[] { 3 }));
        Assert.IsTrue(utils.myTestPrimeFactors(4).SequenceEqual(new int[] { 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(5).SequenceEqual(new int[] { 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(6).SequenceEqual(new int[] { 2, 3 }));
        Assert.IsTrue(utils.myTestPrimeFactors(7).SequenceEqual(new int[] { 7 }));
        Assert.IsTrue(utils.myTestPrimeFactors(8).SequenceEqual(new int[] { 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(9).SequenceEqual(new int[] { 3, 3 }));
        Assert.IsTrue(utils.myTestPrimeFactors(10).SequenceEqual(new int[] { 2, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(11).SequenceEqual(new int[] { 11 }));
        Assert.IsTrue(utils.myTestPrimeFactors(12).SequenceEqual(new int[] { 2, 2, 3 }));
        Assert.IsTrue(utils.myTestPrimeFactors(13).SequenceEqual(new int[] { 13 }));
        Assert.IsTrue(utils.myTestPrimeFactors(14).SequenceEqual(new int[] { 2, 7 }));
        Assert.IsTrue(utils.myTestPrimeFactors(15).SequenceEqual(new int[] { 3, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(16).SequenceEqual(new int[] { 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(17).SequenceEqual(new int[] { 17 }));
        Assert.IsTrue(utils.myTestPrimeFactors(18).SequenceEqual(new int[] { 2, 3, 3 }));
        Assert.IsTrue(utils.myTestPrimeFactors(19).SequenceEqual(new int[] { 19 }));
        Assert.IsTrue(utils.myTestPrimeFactors(20).SequenceEqual(new int[] { 2, 2, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(30).SequenceEqual(new int[] { 2, 3, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(40).SequenceEqual(new int[] { 2, 2, 2, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(50).SequenceEqual(new int[] { 2, 5, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(60).SequenceEqual(new int[] { 2, 2, 3, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(70).SequenceEqual(new int[] { 2, 5, 7 }));
        Assert.IsTrue(utils.myTestPrimeFactors(80).SequenceEqual(new int[] { 2, 2, 2, 2, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(90).SequenceEqual(new int[] { 2, 3, 3, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(100).SequenceEqual(new int[] { 2, 2, 5, 5 }));
        Assert.IsTrue(utils.myTestPrimeFactors(4).SequenceEqual(new int[] { 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(8).SequenceEqual(new int[] { 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(16).SequenceEqual(new int[] { 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(32).SequenceEqual(new int[] { 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(64).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(128).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(512).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(1024).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(2048).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(4096).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(8192).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(16384).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(32768).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(65536).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(131072).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(262144).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
        Assert.IsTrue(utils.myTestPrimeFactors(524288).SequenceEqual(new int[] { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }));
    }

    [TestMethod]
    public void test_lcm() {
        Assert.AreEqual(utils.myTestLcm(new int[] { 0 }), 0);
        Assert.AreEqual(utils.myTestLcm(new int[] { 1 }), 1);
        Assert.AreEqual(utils.myTestLcm(new int[] { 1, 2 }), 2);
        Assert.AreEqual(utils.myTestLcm(new int[] { 1, 3, 2 }), 6);
        Assert.AreEqual(utils.myTestLcm(new int[] { 3, 1, 2 }), 6);
        Assert.AreEqual(utils.myTestLcm(new int[] { 3, 2, 1 }), 6);
        Assert.AreEqual(utils.myTestLcm(new int[] { 2, 3, 4 }), 12);
        Assert.AreEqual(utils.myTestLcm(new int[] { 2, 3, 4, 5 }), 60);
        Assert.AreEqual(utils.myTestLcm(new int[] { 2, 3, 7 }), 42);
        Assert.AreEqual(utils.myTestLcm(new int[] { 2, 4, 8 }), 8);
        Assert.AreEqual(utils.myTestLcm(new int[] { 3, 6, 9 }), 18);
        Assert.AreEqual(utils.myTestLcm(new int[] { 4, 16, 64 }), 64);
        Assert.AreEqual(utils.myTestLcm(new int[] { 16, 4, 64 }), 64);
        Assert.AreEqual(utils.myTestLcm(new int[] { 64, 16, 4 }), 64);
        Assert.AreEqual(utils.myTestLcm(new int[] { 7, 49, 349 }), 17101);
    }

    [TestMethod]
    public void test_hcf() {
        Assert.AreEqual(utils.myTestHcf(new int[] { 5, 15, 30 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 5, 30, 15 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 15, 5, 30 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 15, 30, 5 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 30, 5, 15 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 30, 15, 5 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 21, 35, 7 }), 7);
        Assert.AreEqual(utils.myTestHcf(new int[] { 12, 24, 48 }), 12);
        Assert.AreEqual(utils.myTestHcf(new int[] { 9, 27, 126 }), 9);
        Assert.AreEqual(utils.myTestHcf(new int[] { -5, 15, 30 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 5, -15, 30 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { 5, 15, -30 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { -5, 30, 15 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { -15, 5, 30 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { -15, 30, 5 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { -30, 5, 15 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { -30, 15, 5 }), 5);
        Assert.AreEqual(utils.myTestHcf(new int[] { -21, 35, 7 }), 7);
        Assert.AreEqual(utils.myTestHcf(new int[] { -12, 24, 48 }), 12);
        Assert.AreEqual(utils.myTestHcf(new int[] { -9, 27, 126 }), 9);
    }

    [TestMethod]
    public void test_sq() {
        Assert.AreEqual(utils.myLibSquare(0, 0), 0);
        CollectionAssert.Contains(new int[] { 0, 1 }, utils.myLibSquare(0, 1));
        CollectionAssert.Contains(new int[] { 0, 1 }, utils.myLibSquare(1, 0));
        CollectionAssert.Contains(new int[] { 0, 1 }, utils.myLibSquare(-1, 0));
        CollectionAssert.Contains(new int[] { -1, 0, 1 }, utils.myLibSquare(-1, 1));
        Assert.AreEqual(utils.myLibSquare(1, 1), 1);
        CollectionAssert.Contains(new int[] { 0, 1, 4 }, utils.myLibSquare(-2, 2));
        CollectionAssert.Contains(new int[] { 1, 4 }, utils.myLibSquare(2, 2));
        CollectionAssert.Contains(new int[] { 1, 4 }, utils.myLibSquare(1, 2));
        CollectionAssert.Contains(new int[] { 1, 4, 9 }, utils.myLibSquare(1, 3));
        CollectionAssert.Contains(new int[] { 4, 9, 16, 25 }, utils.myLibSquare(2, 5));
        CollectionAssert.Contains(new int[] { 25, 36, 49, 64, 81, 100 }, utils.myLibSquare(5, 10));
        Assert.IsTrue(notFixated<int>(new int[] { 25 }, utils.myLibSquare, new object[] { 5, 10 }));
        Assert.IsTrue(notFixated<int>(new int[] { 26 }, utils.myLibSquare, new object[] { 5, 10 }));
        Assert.IsTrue(notFixated<int>(new int[] { 49 }, utils.myLibSquare, new object[] { 5, 10 }));
        Assert.IsTrue(notFixated<int>(new int[] { 64 }, utils.myLibSquare, new object[] { 5, 10 }));
        Assert.IsTrue(notFixated<int>(new int[] { 81 }, utils.myLibSquare, new object[] { 5, 10 }));
        Assert.IsTrue(notFixated<int>(new int[] { 100 }, utils.myLibSquare, new object[] { 5, 10 }));
    }

    [TestMethod]
    public void test_is_square() {
        Assert.IsFalse(utils.myLibIsSquare(-1));
        Assert.IsFalse(utils.myLibIsSquare(0));
        Assert.IsFalse(utils.myLibIsSquare(1));
        Assert.IsFalse(utils.myLibIsSquare(2));
        Assert.IsFalse(utils.myLibIsSquare(3));
        Assert.IsTrue(utils.myLibIsSquare(4));
        Assert.IsFalse(utils.myLibIsSquare(-4));
        Assert.IsFalse(utils.myLibIsSquare(5));
        Assert.IsFalse(utils.myLibIsSquare(6));
        Assert.IsFalse(utils.myLibIsSquare(7));
        Assert.IsFalse(utils.myLibIsSquare(8));
        Assert.IsTrue(utils.myLibIsSquare(9));
        Assert.IsFalse(utils.myLibIsSquare(-9));
        Assert.IsFalse(utils.myLibIsSquare(10));
        Assert.IsFalse(utils.myLibIsSquare(-10));
        Assert.IsFalse(utils.myLibIsSquare(11));
        Assert.IsFalse(utils.myLibIsSquare(12));
        Assert.IsFalse(utils.myLibIsSquare(13));
        Assert.IsFalse(utils.myLibIsSquare(14));
        Assert.IsFalse(utils.myLibIsSquare(15));
        Assert.IsTrue(utils.myLibIsSquare(16));
        Assert.IsFalse(utils.myLibIsSquare(17));
        Assert.IsFalse(utils.myLibIsSquare(18));
        Assert.IsFalse(utils.myLibIsSquare(19));
        Assert.IsFalse(utils.myLibIsSquare(20));
        Assert.IsTrue(utils.myLibIsSquare(25));
        Assert.IsFalse(utils.myLibIsSquare(26));
        Assert.IsFalse(utils.myLibIsSquare(35));
        Assert.IsTrue(utils.myLibIsSquare(36));
        Assert.IsFalse(utils.myLibIsSquare(37));
        Assert.IsFalse(utils.myLibIsSquare(48));
        Assert.IsTrue(utils.myLibIsSquare(49));
        Assert.IsFalse(utils.myLibIsSquare(50));
        Assert.IsFalse(utils.myLibIsSquare(51));
        Assert.IsFalse(utils.myLibIsSquare(53));
        Assert.IsTrue(utils.myLibIsSquare(64));
        Assert.IsFalse(utils.myLibIsSquare(65));
        Assert.IsFalse(utils.myLibIsSquare(168));
        Assert.IsTrue(utils.myLibIsSquare(169));
        Assert.IsFalse(utils.myLibIsSquare(170));
    }

    [TestMethod]
    public void test_not_square() {
        // dependency schmendency
        //Assert.IsFalse( utils.myLibIsSquare( utils.myLibNotSquare( 0,   1 ) ) )
        //Assert.IsFalse( utils.myLibIsSquare( utils.myLibNotSquare( 1,   0 ) ) )
        //Assert.IsFalse( utils.myLibIsSquare( utils.myLibNotSquare( -1,   0 ) ) )
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(-1, 1)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(1, 1)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(-2, 2)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(2, 2)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(1, 2)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(1, 3)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(2, 5)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 10)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(-50, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(-50, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(-50, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(-50, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        Assert.IsFalse(utils.myLibIsSquare(utils.myLibNotSquare(5, 100)));
        int rslt = utils.myLibNotSquare(5, 100);
        Assert.IsTrue(notFixated<int>(new int[] { rslt }, utils.myLibNotSquare, new object[ ] {5, 100 } ));
        rslt = utils.myLibNotSquare(-5, 50);
        Assert.IsTrue(notFixated<int>(new int[] { rslt }, utils.myLibNotSquare, new object[] { -5, 50 } ));
        rslt = utils.myLibNotSquare(-5, 5);
        Assert.IsTrue(notFixated<int>(new int[] { rslt }, utils.myLibNotSquare, new object[] { -5, 5 }));
    }

    [TestMethod]
    public void  test_int_choose(){
        CollectionAssert.Contains(new int[] { -1 }, utils.myLibChoose(new int[] { -1, -1 } ) );
        CollectionAssert.Contains(new int[] { -1, 1 }, utils.myLibChoose(new int[] { 1, -1 } ) );
        CollectionAssert.Contains(new int[] { -1, 1 }, utils.myLibChoose(new int[] { -1, 1 } ) );
        CollectionAssert.Contains(new int[] { 1 }, utils.myLibChoose(new int[] { 1, 1 } ) );
        CollectionAssert.Contains(new int[] { 0 }, utils.myLibChoose(new int[] { 0, 0 } ) );
        CollectionAssert.Contains(new int[] { 0, 1 }, utils.myLibChoose(new int[] { 0, 1 } ) );
        CollectionAssert.Contains(new int[] { -1, 0 }, utils.myLibChoose(new int[] { 0, -1 } ) );
        CollectionAssert.Contains(new int[] { 0, 1 }, utils.myLibChoose(new int[] { 1, 0 } ) );
        CollectionAssert.Contains(new int[] { -1, 0 }, utils.myLibChoose(new int[] { -1, 0 } ) );
        CollectionAssert.Contains(new int[] { 10, 11, 12, 13, 14 }, utils.myLibChoose(new int[] { 10, 11, 12, 13, 14 } ) );
        CollectionAssert.Contains(new int[] { 100, 200, 300, 400, 500 }, utils.myLibChoose(new int[] { 100, 200, 300, 400, 500 } ) );
        CollectionAssert.Contains(new double[] { 10, 10.1, 10.2, 10.3, 10.4 }, utils.myLibChoose(new double[] { 10, 10.1, 10.2, 10.3, 10.4 } ) );
        CollectionAssert.Contains(new int[] { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 8192, 16384, 32768, 65536 },
                    utils.myLibChoose(new int[] { 2, 4, 8, 16, 32, 64, 128, 256, 512, 1024, 2048, 8192, 16384, 32768, 65536 }));
    }

    [TestMethod]
    public void  test_int_not() {
        // dependency schmendency

        Assert.AreNotEqual(utils.myLibRunFuncUntilNot<int>(new int[] { 0 }, utils.myTestRi, 250, new object[] { -1, 1, true }), 0);
        Assert.AreNotEqual(utils.myLibRunFuncUntilNot<int>(new int[] { 5 }, utils.myTestRi, 250, new object[] { 4, 6, true }), 5);
        Assert.AreNotEqual(utils.myLibRunFuncUntilNot<int>(new int[] { 5 }, utils.myTestRi, 250, new object[] { 4, 6, true }), 5);
        Assert.AreNotEqual(utils.myLibRunFuncUntilNot<int>(new int[] { 5 }, utils.myTestRi, 250, new object[] { 4, 6, true }), 5);
        Assert.AreNotEqual(utils.myLibRunFuncUntilNot<int>(new int[] { 5 }, utils.myTestRi, 250, new object[] { 4, 6, true }), 5);
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {5,6}, utils.myLibRunFuncUntilNot<int>(new int[ ] {5,6}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] { 5 }, utils.myLibRunFuncUntilNot<int>(new int[ ] { 5 }, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {4,5}, utils.myLibRunFuncUntilNot<int>(new int[ ] {4,5}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {4,5}, utils.myLibRunFuncUntilNot<int>(new int[ ] {4,5}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {4,5}, utils.myLibRunFuncUntilNot<int>(new int[ ] {4,5}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
        CollectionAssert.DoesNotContain(new int[ ] {4,5}, utils.myLibRunFuncUntilNot<int>(new int[ ] {4,5}, utils.myTestRi, 250, new object[] { 4, 6, true }) );
    }

    [TestMethod]
    public void  test_significant_figures() {
        Assert.AreEqual(utils.myLibSignificantFigures(     0.0,     1), 0);
        Assert.AreEqual(utils.myLibSignificantFigures(     1.0,     1), 1);
        Assert.AreEqual(utils.myLibSignificantFigures(     1.1,     1), 1);
        Assert.AreEqual(utils.myLibSignificantFigures(    11.0,     1), 10);
        Assert.AreEqual(utils.myLibSignificantFigures(   111.0,     2), 110);
        Assert.AreEqual(utils.myLibSignificantFigures(  1111.0,     3), 1110);
        Assert.AreEqual(utils.myLibSignificantFigures(    11.1,     1), 10);
        Assert.AreEqual(utils.myLibSignificantFigures(    11.11,    2), 11);
        Assert.AreEqual(utils.myLibSignificantFigures(    11.111,   3), 11.1, 0.00001);
        Assert.AreEqual(utils.myLibSignificantFigures(    11.1111,  4), 11.11);
        Assert.AreEqual(utils.myLibSignificantFigures(     0.1,     1), 0.1);
        Assert.AreEqual(utils.myLibSignificantFigures(     0.01,    1), 0.01);
        Assert.AreEqual(utils.myLibSignificantFigures(     0.001,   1), 0.001);
        Assert.AreEqual(utils.myLibSignificantFigures(     0.0001,  1), 0.0001);
        Assert.AreEqual(utils.myLibSignificantFigures(    -1.0,     1), -1);
        Assert.AreEqual(utils.myLibSignificantFigures(    -1.1,     1), -1);
        Assert.AreEqual(utils.myLibSignificantFigures(   -11.0,     1), -10);
        Assert.AreEqual(utils.myLibSignificantFigures(  -111.0,     2), -110);
        Assert.AreEqual(utils.myLibSignificantFigures( -1111.0,     3), -1110);
        Assert.AreEqual(utils.myLibSignificantFigures(   -11.1,     1), -10);
        Assert.AreEqual(utils.myLibSignificantFigures(   -11.11,    2), -11);
        Assert.AreEqual(utils.myLibSignificantFigures(   -11.111,   3), -11.1, 0.00001);
        Assert.AreEqual(utils.myLibSignificantFigures(   -11.1111,  4), -11.11);
        Assert.AreEqual(utils.myLibSignificantFigures(    -0.1,     1), -0.1);
        Assert.AreEqual(utils.myLibSignificantFigures(    -0.01,    1), -0.01);
        Assert.AreEqual(utils.myLibSignificantFigures(    -0.001,   1), -0.001);
        Assert.AreEqual(utils.myLibSignificantFigures(    -0.0001,  1), -0.0001);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 1), 10000);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 2), 12000);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 3), 12300);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 4), 12350);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 5), 12345);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 6), 12345.1);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 7), 12345.12);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 8), 12345.123);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345, 9), 12345.1235);
        Assert.AreEqual(utils.myLibSignificantFigures( 12345.12345,10), 12345.12345, 0.00001);
        Assert.AreEqual(utils.myLibSignificantFigures(   123.123,   8), 123.123);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 1), -10000);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 2), -12000);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 3), -12300);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 4), -12350);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 5), -12345);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 6), -12345.1);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 7), -12345.12);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 8), -12345.123);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345, 9), -12345.1235);
        Assert.AreEqual(utils.myLibSignificantFigures(-12345.12345,10), -12345.12345, 0.00001);
        Assert.AreEqual(utils.myLibSignificantFigures(-123.123, 8), -123.123);
    }

    //[TestMethod]
    public void  test_to_sub() {
        // Assert.Equals(Utils.mylib_to_sub("ABCpublic void GHIJKLMNOPQRSTUVWXYZabcpublic void ghijklmnopqrstuvwxyz0123456789+-=()"), \
        //                               "ₐ₈CDₑբ₉ₕᵢⱼₖₗₘₙₒₚQᵣₛₜᵤᵥwₓᵧZₐ♭꜀ᑯₑբ₉ₕᵢⱼₖₗₘₙₒₚ૧ᵣₛₜᵤᵥwₓᵧ₂₀₁₂₃₄₅₆₇₈₉₊₋₌₍₎")
        Assert.AreEqual(utils.myLibToSub("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"), 
            "ₐ₈CDₑբ₉ₕᵢⱼₖₗₘₙₒₚQᵣₛₜᵤᵥwₓᵧZₐ♭꜀ᑯₑբ₉ₕᵢⱼₖₗₘₙₒₚ૧ᵣₛₜᵤᵥwₓᵧ₂₀₁₂₃₄₅₆₇₈₉");
    }

    //[TestMethod]
    public void  test_to_super() {
        //Assert.Equals(Utils.mylib_to_super("ABCpublic void GHIJKLMNOPQRSTUVWXYZabcpublic void ghijklmnopqrstuvwxyz0123456789+-=()"), \
        //                             "ᴬᴮᶜᴰᴱᶠᴳᴴᴵᴶᴷᴸᴹᴺᴼᴾQᴿˢᵀᵁⱽᵂˣʸᶻᵃᵇᶜᵈᵉᶠᵍʰᶦʲᵏˡᵐⁿᵒᵖ۹ʳˢᵗᵘᵛʷˣʸᶻ⁰¹²³⁴⁵⁶⁷⁸⁹⁺⁻⁼⁽⁾")
        Assert.AreEqual(utils.myLibToSup("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"),
            "ᴬᴮᶜᴰᴱᶠᴳᴴᴵᴶᴷᴸᴹᴺᴼᴾQᴿˢᵀᵁⱽᵂˣʸᶻᵃᵇᶜᵈᵉᶠᵍʰᶦʲᵏˡᵐⁿᵒᵖ۹ʳˢᵗᵘᵛʷˣʸᶻ⁰¹²³⁴⁵⁶⁷⁸⁹");
    }

    [TestMethod]
    public void  test_to_standard_form() {
        Assert.AreEqual(utils.myLibToStandardForm(111.11), "1.1111*10⁻²");
        Assert.AreEqual(utils.myLibToStandardForm(0.11111), "1.1111*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(-0.11111), "-1.1111*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(0), "0");
        Assert.AreEqual(utils.myLibToStandardForm(1), "1");
        Assert.AreEqual(utils.myLibToStandardForm(-1), "-1");
        Assert.AreEqual(utils.myLibToStandardForm(0.00012345), "1.2345*10⁴"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(0.0012345), "1.2345*10³"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(0.012345), "1.2345*10²"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(0.12345), "1.2345*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(1.2345), "1.2345");
        Assert.AreEqual(utils.myLibToStandardForm(12.345), "1.2345*10⁻¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(123.45), "1.2345*10⁻²"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(1234.5), "1.2345*10⁻³"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(12345.0), "1.2345*10⁻⁴"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-111.11), "-1.1111*10⁻²");
        Assert.AreEqual(utils.myLibToStandardForm(-0.11111), "-1.1111*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(-0.00012345), "-1.2345*10⁴"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-0.0012345), "-1.2345*10³"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-0.012345), "-1.2345*10²"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-0.12345), "-1.2345*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-1.2345), "-1.2345");
        Assert.AreEqual(utils.myLibToStandardForm(-12.345), "-1.2345*10⁻¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-123.45), "-1.2345*10⁻²"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-1234.5), "-1.2345*10⁻³"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-12345.0), "-1.2345*10⁻⁴"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(9.9), "9.9");
        Assert.AreEqual(utils.myLibToStandardForm(-9.9), "-9.9");
        Assert.AreEqual(utils.myLibToStandardForm(10), "1.0*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(10.0), "1.0*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(-10), "-1.0*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(-10.0), "-1.0*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(10.01), "1.001*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(-10.01), "-1.001*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(11), "1.1*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(11.0), "1.1*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(-11), "-1.1*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(-11.0), "-1.1*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(11.01), "1.101*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(-11.01), "-1.101*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(0.9), "9*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(0.09), "9*10²");
        Assert.AreEqual(utils.myLibToStandardForm(0.009), "9*10³");
        Assert.AreEqual(utils.myLibToStandardForm(0.0009), "9*10⁴");
        Assert.AreEqual(utils.myLibToStandardForm(0.00009), "9.0*10⁵"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(-0.9), "-9*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(-0.09), "-9*10²");
        Assert.AreEqual(utils.myLibToStandardForm(-0.009), "-9*10³");
        Assert.AreEqual(utils.myLibToStandardForm(-0.0009), "-9*10⁴");
        Assert.AreEqual(utils.myLibToStandardForm(-0.00009), "-9.0*10⁵"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(    5           ), "5");
        Assert.AreEqual(utils.myLibToStandardForm(    55          ), "5.5*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(    555         ), "5.55*10⁻²");
        Assert.AreEqual(utils.myLibToStandardForm(    5555        ), "5.555*10⁻³");
        Assert.AreEqual(utils.myLibToStandardForm( 55555       ), "5.5555*10⁻⁴"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 555555      ), "5.55555*10⁻⁵"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 5555555     ), "5.555555*10⁻⁶"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 55555555    ), "5.5555555*10⁻⁷"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 555555555   ), "5.55555555*10⁻⁸"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(   -5           ), "-5");
        Assert.AreEqual(utils.myLibToStandardForm(   -55          ), "-5.5*10⁻¹");
        Assert.AreEqual(utils.myLibToStandardForm(   -555         ), "-5.55*10⁻²");
        Assert.AreEqual(utils.myLibToStandardForm(   -5555        ), "-5.555*10⁻³");
        Assert.AreEqual(utils.myLibToStandardForm( -55555      ), "-5.5555*10⁻⁴"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -555555     ), "-5.55555*10⁻⁵"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -5555555    ), "-5.555555*10⁻⁶"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -55555555   ), "-5.5555555*10⁻⁷"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -555555555  ), "-5.55555555*10⁻⁸"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(    0.5         ), "5*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(    0.55        ), "5.5*10¹");
        Assert.AreEqual(utils.myLibToStandardForm( 0.555       ), "5.55*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 0.5555      ), "5.555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 0.55555     ), "5.5555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 0.555555    ), "5.55555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 0.5555555   ), "5.555555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 0.55555556  ), "5.5555556*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( 0.555555555 ), "5.55555555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm(   -0.5         ), "-5*10¹");
        Assert.AreEqual(utils.myLibToStandardForm(   -0.55        ), "-5.5*10¹");
        Assert.AreEqual(utils.myLibToStandardForm( -0.555       ), "-5.55*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -0.5555      ), "-5.555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -0.55555     ), "-5.5555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -0.555555    ), "-5.55555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -0.5555555   ), "-5.555555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -0.55555555  ), "-5.5555555*10¹"); //, 0.00000000001);
        Assert.AreEqual(utils.myLibToStandardForm( -0.555555555 ), "-5.55555555*10¹"); //, 0.00000000001);
    }
}