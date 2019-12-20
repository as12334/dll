namespace Agent.Web.L_SIX
{
    using Agent.Web.WebBase;
    using LotterySystem.Common;
    using LotterySystem.Model;
    using System;
    using System.Data;

    public class Lottery_Add : MemberPageBase
    {
        protected string txtEndDate = "";
        protected string txtEndTime = "";
        protected string txtOpenDate = "";
        protected string txtOpenTime = "";
        protected string txtPhase = "";
        protected string txtTMEndDate = "";
        protected string txtTMEndTime = "";
        protected agent_userinfo_session userModel;

        private void AddPhase(cz_phase_six phaseModel)
        {
            DataSet playByOddsSet = CallBLL.cz_play_six_bll.GetPlayByOddsSet("91025,91026,91027,91028,91029");
            if (((playByOddsSet != null) && (playByOddsSet.Tables.Count > 0)) && (playByOddsSet.Tables[0].Rows.Count > 0))
            {
                DataTable table = playByOddsSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    int num;
                    cz_play_six _six = new cz_play_six();
                    string str = row["play_id"].ToString();
                    string str2 = row["default_odds"].ToString();
                    string str3 = row["max_odds"].ToString();
                    string str4 = row["min_odds"].ToString();
                    row["a_diff"].ToString();
                    string str5 = row["b_diff"].ToString();
                    string str6 = row["c_diff"].ToString();
                    _six.set_play_id(Convert.ToInt32(str));
                    _six.set_default_odds(str2);
                    _six.set_max_odds(str3);
                    _six.set_min_odds(str4);
                    _six.set_b_diff(str5);
                    _six.set_c_diff(str6);
                    if (CallBLL.cz_bet_six_bll.GetIspayment() > 0)
                    {
                        num = 1;
                    }
                    else
                    {
                        num = 0;
                    }
                    try
                    {
                        if (_six.get_play_id().Equals(0x16385))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SB(_six, num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x16382))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_ZT(_six, "91010,91025,91026,91027,91028,91029", num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x1637e))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_TMSX(_six, base.get_YearLian(), num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x1637f))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_TMSB(_six, num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x1638d))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "sx", base.get_YearLian(), num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x1638e))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "ws", "0", num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x163ae))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "sx", base.get_YearLian(), num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x163af))
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_SXWS(_six, "ws", "0", num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x16380))
                        {
                            string[] strArray = _six.get_default_odds().Split(new char[] { '|' });
                            string[] strArray2 = _six.get_max_odds().Split(new char[] { '|' });
                            string[] strArray3 = _six.get_min_odds().Split(new char[] { '|' });
                            string[] strArray4 = _six.get_b_diff().Split(new char[] { '|' });
                            string[] strArray5 = _six.get_c_diff().Split(new char[] { '|' });
                            cz_odds_six _six2 = new cz_odds_six();
                            _six2.set_play_id(new int?(_six.get_play_id()));
                            _six2.set_play_name("半波【單雙】");
                            _six2.set_put_amount("紅波單");
                            _six2.set_current_odds(strArray[0]);
                            _six2.set_max_odds(strArray2[0]);
                            _six2.set_min_odds(strArray3[0]);
                            _six2.set_b_diff(strArray4[0]);
                            _six2.set_c_diff(strArray5[0]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six2, num.ToString());
                            cz_odds_six _six3 = new cz_odds_six();
                            _six3.set_play_id(new int?(_six.get_play_id()));
                            _six3.set_play_name("半波【單雙】");
                            _six3.set_put_amount("紅波雙");
                            _six3.set_current_odds(strArray[1]);
                            _six3.set_max_odds(strArray2[1]);
                            _six3.set_min_odds(strArray3[1]);
                            _six3.set_b_diff(strArray4[1]);
                            _six3.set_c_diff(strArray5[1]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six3, num.ToString());
                            cz_odds_six _six4 = new cz_odds_six();
                            _six4.set_play_id(new int?(_six.get_play_id()));
                            _six4.set_play_name("半波【單雙】");
                            _six4.set_put_amount("藍波單");
                            _six4.set_current_odds(strArray[2]);
                            _six4.set_max_odds(strArray2[2]);
                            _six4.set_min_odds(strArray3[2]);
                            _six4.set_b_diff(strArray4[2]);
                            _six4.set_c_diff(strArray5[2]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six4, num.ToString());
                            cz_odds_six _six5 = new cz_odds_six();
                            _six5.set_play_id(new int?(_six.get_play_id()));
                            _six5.set_play_name("半波【單雙】");
                            _six5.set_put_amount("藍波雙");
                            _six5.set_current_odds(strArray[3]);
                            _six5.set_max_odds(strArray2[3]);
                            _six5.set_min_odds(strArray3[3]);
                            _six5.set_b_diff(strArray4[3]);
                            _six5.set_c_diff(strArray5[3]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six5, num.ToString());
                            cz_odds_six _six6 = new cz_odds_six();
                            _six6.set_play_id(new int?(_six.get_play_id()));
                            _six6.set_play_name("半波【單雙】");
                            _six6.set_put_amount("綠波單");
                            _six6.set_current_odds(strArray[4]);
                            _six6.set_max_odds(strArray2[4]);
                            _six6.set_min_odds(strArray3[4]);
                            _six6.set_b_diff(strArray4[4]);
                            _six6.set_c_diff(strArray5[4]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six6, num.ToString());
                            cz_odds_six _six7 = new cz_odds_six();
                            _six7.set_play_id(new int?(_six.get_play_id()));
                            _six7.set_play_name("半波【單雙】");
                            _six7.set_put_amount("綠波雙");
                            _six7.set_current_odds(strArray[5]);
                            _six7.set_max_odds(strArray2[5]);
                            _six7.set_min_odds(strArray3[5]);
                            _six7.set_b_diff(strArray4[5]);
                            _six7.set_c_diff(strArray5[5]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six7, num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x163b1))
                        {
                            string[] strArray6 = _six.get_default_odds().Split(new char[] { '|' });
                            string[] strArray7 = _six.get_max_odds().Split(new char[] { '|' });
                            string[] strArray8 = _six.get_min_odds().Split(new char[] { '|' });
                            string[] strArray9 = _six.get_b_diff().Split(new char[] { '|' });
                            string[] strArray10 = _six.get_c_diff().Split(new char[] { '|' });
                            cz_odds_six _six8 = new cz_odds_six();
                            _six8.set_play_id(new int?(_six.get_play_id()));
                            _six8.set_play_name("半波【大小】");
                            _six8.set_put_amount("紅波大");
                            _six8.set_current_odds(strArray6[0]);
                            _six8.set_max_odds(strArray7[0]);
                            _six8.set_min_odds(strArray8[0]);
                            _six8.set_b_diff(strArray9[0]);
                            _six8.set_c_diff(strArray10[0]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six8, num.ToString());
                            cz_odds_six _six9 = new cz_odds_six();
                            _six9.set_play_id(new int?(_six.get_play_id()));
                            _six9.set_play_name("半波【大小】");
                            _six9.set_put_amount("紅波小");
                            _six9.set_current_odds(strArray6[1]);
                            _six9.set_max_odds(strArray7[1]);
                            _six9.set_min_odds(strArray8[1]);
                            _six9.set_b_diff(strArray9[1]);
                            _six9.set_c_diff(strArray10[1]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six9, num.ToString());
                            cz_odds_six _six10 = new cz_odds_six();
                            _six10.set_play_id(new int?(_six.get_play_id()));
                            _six10.set_play_name("半波【大小】");
                            _six10.set_put_amount("藍波大");
                            _six10.set_current_odds(strArray6[2]);
                            _six10.set_max_odds(strArray7[2]);
                            _six10.set_min_odds(strArray8[2]);
                            _six10.set_b_diff(strArray9[2]);
                            _six10.set_c_diff(strArray10[2]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six10, num.ToString());
                            cz_odds_six _six11 = new cz_odds_six();
                            _six11.set_play_id(new int?(_six.get_play_id()));
                            _six11.set_play_name("半波【大小】");
                            _six11.set_put_amount("藍波小");
                            _six11.set_current_odds(strArray6[3]);
                            _six11.set_max_odds(strArray7[3]);
                            _six11.set_min_odds(strArray8[3]);
                            _six11.set_b_diff(strArray9[3]);
                            _six11.set_c_diff(strArray10[3]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six11, num.ToString());
                            cz_odds_six _six12 = new cz_odds_six();
                            _six12.set_play_id(new int?(_six.get_play_id()));
                            _six12.set_play_name("半波【大小】");
                            _six12.set_put_amount("綠波大");
                            _six12.set_current_odds(strArray6[4]);
                            _six12.set_max_odds(strArray7[4]);
                            _six12.set_min_odds(strArray8[4]);
                            _six12.set_b_diff(strArray9[4]);
                            _six12.set_c_diff(strArray10[4]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six12, num.ToString());
                            cz_odds_six _six13 = new cz_odds_six();
                            _six13.set_play_id(new int?(_six.get_play_id()));
                            _six13.set_play_name("半波【大小】");
                            _six13.set_put_amount("綠波小");
                            _six13.set_current_odds(strArray6[5]);
                            _six13.set_max_odds(strArray7[5]);
                            _six13.set_min_odds(strArray8[5]);
                            _six13.set_b_diff(strArray9[5]);
                            _six13.set_c_diff(strArray10[5]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_BB(_six13, num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x163ac))
                        {
                            string[] strArray11 = _six.get_default_odds().Split(new char[] { '|' });
                            string[] strArray12 = _six.get_max_odds().Split(new char[] { '|' });
                            string[] strArray13 = _six.get_min_odds().Split(new char[] { '|' });
                            string[] strArray14 = _six.get_b_diff().Split(new char[] { '|' });
                            string[] strArray15 = _six.get_c_diff().Split(new char[] { '|' });
                            cz_odds_six _six14 = new cz_odds_six();
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單0、雙7");
                            _six14.set_current_odds(strArray11[0]);
                            _six14.set_max_odds(strArray12[0]);
                            _six14.set_min_odds(strArray13[0]);
                            _six14.set_b_diff(strArray14[0]);
                            _six14.set_c_diff(strArray15[0]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單1、雙6");
                            _six14.set_current_odds(strArray11[1]);
                            _six14.set_max_odds(strArray12[1]);
                            _six14.set_min_odds(strArray13[1]);
                            _six14.set_b_diff(strArray14[1]);
                            _six14.set_c_diff(strArray15[1]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單2、雙5");
                            _six14.set_current_odds(strArray11[2]);
                            _six14.set_max_odds(strArray12[2]);
                            _six14.set_min_odds(strArray13[2]);
                            _six14.set_b_diff(strArray14[2]);
                            _six14.set_c_diff(strArray15[2]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單3、雙4");
                            _six14.set_current_odds(strArray11[3]);
                            _six14.set_max_odds(strArray12[3]);
                            _six14.set_min_odds(strArray13[3]);
                            _six14.set_b_diff(strArray14[3]);
                            _six14.set_c_diff(strArray15[3]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單4、雙3");
                            _six14.set_current_odds(strArray11[4]);
                            _six14.set_max_odds(strArray12[4]);
                            _six14.set_min_odds(strArray13[4]);
                            _six14.set_b_diff(strArray14[4]);
                            _six14.set_c_diff(strArray15[4]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單5、雙2");
                            _six14.set_current_odds(strArray11[5]);
                            _six14.set_max_odds(strArray12[5]);
                            _six14.set_min_odds(strArray13[5]);
                            _six14.set_b_diff(strArray14[5]);
                            _six14.set_c_diff(strArray15[5]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單6、雙1");
                            _six14.set_current_odds(strArray11[6]);
                            _six14.set_max_odds(strArray12[6]);
                            _six14.set_min_odds(strArray13[6]);
                            _six14.set_b_diff(strArray14[6]);
                            _six14.set_c_diff(strArray15[6]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                            _six14.set_play_id(new int?(_six.get_play_id()));
                            _six14.set_play_name("七碼單雙");
                            _six14.set_put_amount("單7、雙0");
                            _six14.set_current_odds(strArray11[7]);
                            _six14.set_max_odds(strArray12[7]);
                            _six14.set_min_odds(strArray13[7]);
                            _six14.set_b_diff(strArray14[7]);
                            _six14.set_c_diff(strArray15[7]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six14, num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x163b0))
                        {
                            string[] strArray16 = _six.get_default_odds().Split(new char[] { '|' });
                            string[] strArray17 = _six.get_max_odds().Split(new char[] { '|' });
                            string[] strArray18 = _six.get_min_odds().Split(new char[] { '|' });
                            string[] strArray19 = _six.get_b_diff().Split(new char[] { '|' });
                            string[] strArray20 = _six.get_c_diff().Split(new char[] { '|' });
                            cz_odds_six _six15 = new cz_odds_six();
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大0、小7");
                            _six15.set_current_odds(strArray16[0]);
                            _six15.set_max_odds(strArray17[0]);
                            _six15.set_min_odds(strArray18[0]);
                            _six15.set_b_diff(strArray19[0]);
                            _six15.set_c_diff(strArray20[0]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大1、小6");
                            _six15.set_current_odds(strArray16[1]);
                            _six15.set_max_odds(strArray17[1]);
                            _six15.set_min_odds(strArray18[1]);
                            _six15.set_b_diff(strArray19[1]);
                            _six15.set_c_diff(strArray20[1]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大2、小5");
                            _six15.set_current_odds(strArray16[2]);
                            _six15.set_max_odds(strArray17[2]);
                            _six15.set_min_odds(strArray18[2]);
                            _six15.set_b_diff(strArray19[2]);
                            _six15.set_c_diff(strArray20[2]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大3、小4");
                            _six15.set_current_odds(strArray16[3]);
                            _six15.set_max_odds(strArray17[3]);
                            _six15.set_min_odds(strArray18[3]);
                            _six15.set_b_diff(strArray19[3]);
                            _six15.set_c_diff(strArray20[3]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大4、小3");
                            _six15.set_current_odds(strArray16[4]);
                            _six15.set_max_odds(strArray17[4]);
                            _six15.set_min_odds(strArray18[4]);
                            _six15.set_b_diff(strArray19[4]);
                            _six15.set_c_diff(strArray20[4]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大5、小2");
                            _six15.set_current_odds(strArray16[5]);
                            _six15.set_max_odds(strArray17[5]);
                            _six15.set_min_odds(strArray18[5]);
                            _six15.set_b_diff(strArray19[5]);
                            _six15.set_c_diff(strArray20[5]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大6、小1");
                            _six15.set_current_odds(strArray16[6]);
                            _six15.set_max_odds(strArray17[6]);
                            _six15.set_min_odds(strArray18[6]);
                            _six15.set_b_diff(strArray19[6]);
                            _six15.set_c_diff(strArray20[6]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                            _six15.set_play_id(new int?(_six.get_play_id()));
                            _six15.set_play_name("七碼大小");
                            _six15.set_put_amount("大7、小0");
                            _six15.set_current_odds(strArray16[7]);
                            _six15.set_max_odds(strArray17[7]);
                            _six15.set_min_odds(strArray18[7]);
                            _six15.set_b_diff(strArray19[7]);
                            _six15.set_c_diff(strArray20[7]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_QM(_six15, num.ToString());
                        }
                        else if (_six.get_play_id().Equals(0x163ad))
                        {
                            string[] strArray21 = _six.get_default_odds().Split(new char[] { '|' });
                            string[] strArray22 = _six.get_max_odds().Split(new char[] { '|' });
                            string[] strArray23 = _six.get_min_odds().Split(new char[] { '|' });
                            string[] strArray24 = _six.get_b_diff().Split(new char[] { '|' });
                            string[] strArray25 = _six.get_c_diff().Split(new char[] { '|' });
                            cz_odds_six _six16 = new cz_odds_six();
                            _six16.set_play_id(new int?(_six.get_play_id()));
                            _six16.set_play_name("五行");
                            _six16.set_put_amount("金");
                            _six16.set_current_odds(strArray21[0]);
                            _six16.set_max_odds(strArray22[0]);
                            _six16.set_min_odds(strArray23[0]);
                            _six16.set_b_diff(strArray24[0]);
                            _six16.set_c_diff(strArray25[0]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six16, num.ToString());
                            _six16.set_play_id(new int?(_six.get_play_id()));
                            _six16.set_play_name("五行");
                            _six16.set_put_amount("木");
                            _six16.set_current_odds(strArray21[1]);
                            _six16.set_max_odds(strArray22[1]);
                            _six16.set_min_odds(strArray23[1]);
                            _six16.set_b_diff(strArray24[1]);
                            _six16.set_c_diff(strArray25[1]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six16, num.ToString());
                            _six16.set_play_id(new int?(_six.get_play_id()));
                            _six16.set_play_name("五行");
                            _six16.set_put_amount("水");
                            _six16.set_current_odds(strArray21[2]);
                            _six16.set_max_odds(strArray22[2]);
                            _six16.set_min_odds(strArray23[2]);
                            _six16.set_b_diff(strArray24[2]);
                            _six16.set_c_diff(strArray25[2]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six16, num.ToString());
                            _six16.set_play_id(new int?(_six.get_play_id()));
                            _six16.set_play_name("五行");
                            _six16.set_put_amount("火");
                            _six16.set_current_odds(strArray21[3]);
                            _six16.set_max_odds(strArray22[3]);
                            _six16.set_min_odds(strArray23[3]);
                            _six16.set_b_diff(strArray24[3]);
                            _six16.set_c_diff(strArray25[3]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six16, num.ToString());
                            _six16.set_play_id(new int?(_six.get_play_id()));
                            _six16.set_play_name("五行");
                            _six16.set_put_amount("土");
                            _six16.set_current_odds(strArray21[4]);
                            _six16.set_max_odds(strArray22[4]);
                            _six16.set_min_odds(strArray23[4]);
                            _six16.set_b_diff(strArray24[4]);
                            _six16.set_c_diff(strArray25[4]);
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet_WX(_six16, num.ToString());
                        }
                        else
                        {
                            CallBLL.cz_odds_six_bll.SetOddsByOddsSet(_six, num.ToString());
                        }
                    }
                    catch (Exception)
                    {
                        base.Response.Write(base.ShowDialogBox("添加獎期錯誤！", "", 2));
                        base.Response.End();
                    }
                }
                if (CallBLL.cz_phase_six_bll.AddPhase(phaseModel) > 0)
                {
                    cz_lotteryopen_log _log = new cz_lotteryopen_log();
                    string str7 = null;
                    if (this.userModel.get_users_child_session() != null)
                    {
                        str7 = this.userModel.get_users_child_session().get_u_name();
                    }
                    string str8 = "";
                    string str9 = ((("本期編號： " + phaseModel.get_phase()) + "<br />開獎時間： " + Convert.ToDateTime(phaseModel.get_open_date()).ToString("yyyy-MM-dd HH:mm:ss")) + "<br />截止投注時間： " + Convert.ToDateTime(phaseModel.get_stop_date()).ToString("yyyy-MM-dd HH:mm:ss")) + "<br />特碼投注截止： " + Convert.ToDateTime(phaseModel.get_sn_stop_date()).ToString("yyyy-MM-dd HH:mm:ss");
                    _log.set_phase_id(CallBLL.cz_phase_six_bll.GetModelByPhase(phaseModel.get_phase()).get_p_id());
                    _log.set_phase(phaseModel.get_phase());
                    _log.set_u_name(this.userModel.get_u_name());
                    _log.set_children_name(str7);
                    _log.set_action("新增獎期");
                    _log.set_old_val(str8);
                    _log.set_new_val(str9);
                    _log.set_ip(LSRequest.GetIP());
                    _log.set_add_time(DateTime.Now);
                    _log.set_note(string.Format("【本期編號:{0}】新增獎期", _log.get_phase()));
                    _log.set_type_id(0);
                    _log.set_lottery_id(100);
                    CallBLL.cz_lotteryopen_log_bll.Insert(_log);
                    CallBLL.cz_lm_number_amount_six_bll.ClearNumberAmount();
                    if (CallBLL.cz_system_set_six_bll.GetSystemSet(100).get_single_number_isdown().Equals(1))
                    {
                        CallBLL.cz_lm_number_amount_six_bll.SetWtValueAmount();
                    }
                    string url = "/LotteryPeriod/AwardPeriod.aspx?lid=" + 100;
                    FileCacheHelper.UpdateAudoJPFile();
                    base.Response.Write(base.ShowDialogBox("添加獎期成功！", url, 0));
                    base.Response.End();
                }
                else
                {
                    base.Response.Write(base.ShowDialogBox("添加獎期錯誤！", "", 400));
                    base.Response.End();
                }
            }
        }

        private void GetSystemSet()
        {
            cz_system_set_six systemSet = CallBLL.cz_system_set_six_bll.GetSystemSet(100);
            this.txtOpenTime = Convert.ToDateTime(systemSet.get_ev_3()).ToString("HH:mm:ss");
            this.txtEndTime = Convert.ToDateTime(systemSet.get_ev_4()).ToString("HH:mm:ss");
            this.txtTMEndTime = Convert.ToDateTime(systemSet.get_ev_5()).ToString("HH:mm:ss");
        }

        private void IsNoOpenLottery()
        {
            string str = base.Server.UrlEncode(string.Format("/LotteryPeriod/AwardPeriod.aspx?lid={0}", 100));
            if (CallBLL.cz_bet_six_bll.GetIspayment() > 0)
            {
                base.Response.Redirect(string.Format("/MessagePage.aspx?code=u100050&url={0}&issuccess=1&isback=1", str));
                base.Response.End();
            }
            cz_phase_six currentPhase = CallBLL.cz_phase_six_bll.GetCurrentPhase();
            if (currentPhase != null)
            {
                int num2 = Convert.ToInt32(currentPhase.get_is_drawlottery());
                int num3 = Convert.ToInt32(currentPhase.get_is_opendata());
                if ((num2 == 1) || (num3 == 0))
                {
                    base.Response.Redirect(string.Format("/MessagePage.aspx?code=u100050&url={0}&issuccess=1&isback=1", str));
                    base.Response.End();
                }
                int num4 = int.Parse(currentPhase.get_phase()) + 1;
                if ((num4 > 9) && (num4 < 100))
                {
                    this.txtPhase = "0";
                }
                if (num4 < 10)
                {
                    this.txtPhase = "00";
                }
                this.txtPhase = this.txtPhase + num4.ToString();
            }
            else
            {
                this.txtPhase = "001";
            }
            this.txtOpenDate = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtEndDate = DateTime.Now.ToString("yyyy-MM-dd");
            this.txtTMEndDate = DateTime.Now.ToString("yyyy-MM-dd");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = this.Session["user_name"].ToString();
            this.userModel = this.Session[str + "lottery_session_user_info"] as agent_userinfo_session;
            if (!this.userModel.get_u_type().Trim().Equals("zj"))
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100014&url=&issuccess=1&isback=0");
            }
            base.Permission_Aspx_ZJ(this.userModel, "po_3_1");
            if (base.IsChildSync())
            {
                base.Response.Redirect("/MessagePage.aspx?code=u100080&url=&issuccess=1&isback=0");
            }
            this.IsNoOpenLottery();
            this.GetSystemSet();
            if (LSRequest.qq("hdnSubmit").Equals("add"))
            {
                string message = "";
                if (!this.ValidParam(ref message))
                {
                    base.Response.Write(base.ShowDialogBox(message, "", 400));
                    base.Response.End();
                }
                else
                {
                    this.txtPhase = LSRequest.qq("txtPhase");
                    this.txtOpenDate = LSRequest.qq("txtOpenDate");
                    this.txtEndDate = LSRequest.qq("txtEndDate");
                    this.txtTMEndDate = LSRequest.qq("txtTMEndDate");
                    this.txtOpenTime = LSRequest.qq("txtOpenTime");
                    this.txtEndTime = LSRequest.qq("txtEndTime");
                    this.txtTMEndTime = LSRequest.qq("txtTMEndTime");
                    if (CallBLL.cz_phase_six_bll.GetModelByPhase(this.txtPhase) != null)
                    {
                        base.Response.Write(base.ShowDialogBox("已存在該期期號！", "", 2));
                        base.Response.End();
                    }
                    cz_phase_six phaseModel = new cz_phase_six();
                    phaseModel.set_phase(this.txtPhase);
                    phaseModel.set_add_date(new DateTime?(DateTime.Now));
                    phaseModel.set_open_date(new DateTime?(Convert.ToDateTime(this.txtOpenDate + " " + this.txtOpenTime)));
                    phaseModel.set_stop_date(new DateTime?(Convert.ToDateTime(this.txtEndDate + " " + this.txtEndTime)));
                    phaseModel.set_sn_stop_date(new DateTime?(Convert.ToDateTime(this.txtTMEndDate + " " + this.txtTMEndTime)));
                    phaseModel.set_is_closed(0);
                    phaseModel.set_is_drawlottery(1);
                    phaseModel.set_is_opendata(0);
                    phaseModel.set_query_date(new DateTime?(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"))));
                    this.AddPhase(phaseModel);
                }
            }
        }

        private bool ValidParam(ref string message)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(LSRequest.qq("txtPhase")))
            {
                if (!base.IsNumber(LSRequest.qq("txtPhase")))
                {
                    message = "輸入獎期錯誤！";
                    return false;
                }
                flag = true;
            }
            else
            {
                message = "輸入獎期錯誤！";
                return false;
            }
            if (!string.IsNullOrEmpty(LSRequest.qq("txtOpenDate")))
            {
                try
                {
                    Convert.ToDateTime(LSRequest.qq("txtOpenDate"));
                    flag = true;
                    if (!string.IsNullOrEmpty(LSRequest.qq("txtOpenTime")))
                    {
                        try
                        {
                            Convert.ToDateTime(LSRequest.qq("txtOpenTime"));
                            flag = true;
                            if (!string.IsNullOrEmpty(LSRequest.qq("txtEndDate")))
                            {
                                try
                                {
                                    Convert.ToDateTime(LSRequest.qq("txtEndDate"));
                                    flag = true;
                                    if (!string.IsNullOrEmpty(LSRequest.qq("txtEndTime")))
                                    {
                                        try
                                        {
                                            Convert.ToDateTime(LSRequest.qq("txtEndTime"));
                                            flag = true;
                                            if (!string.IsNullOrEmpty(LSRequest.qq("txtTMEndDate")))
                                            {
                                                try
                                                {
                                                    Convert.ToDateTime(LSRequest.qq("txtTMEndDate"));
                                                    flag = true;
                                                    if (!string.IsNullOrEmpty(LSRequest.qq("txtTMEndTime")))
                                                    {
                                                        try
                                                        {
                                                            Convert.ToDateTime(LSRequest.qq("txtTMEndTime"));
                                                            return true;
                                                        }
                                                        catch
                                                        {
                                                            message = "特碼投注截止時間格式錯誤！";
                                                            return false;
                                                        }
                                                    }
                                                    message = "請輸入特碼投注截止時間！";
                                                    return false;
                                                }
                                                catch
                                                {
                                                    message = "特碼投注截止時間格式錯誤！";
                                                    return false;
                                                }
                                            }
                                            message = "請輸入特碼投注截止時間！";
                                            return false;
                                        }
                                        catch
                                        {
                                            message = "截止投注時間格式錯誤！";
                                            return false;
                                        }
                                    }
                                    message = "請輸入投注時間！";
                                    return false;
                                }
                                catch
                                {
                                    message = "截止投注時間格式錯誤！";
                                    return false;
                                }
                            }
                            message = "請輸入投注時間！";
                            return false;
                        }
                        catch
                        {
                            message = "開獎時間格式錯誤！";
                            return false;
                        }
                    }
                    message = "請輸入開獎時間！";
                    return false;
                }
                catch
                {
                    message = "開獎時間格式錯誤！";
                    return false;
                }
            }
            message = "請輸入開獎時間！";
            return false;
        }
    }
}

