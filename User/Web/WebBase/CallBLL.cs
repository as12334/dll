namespace User.Web.WebBase
{
    using LotterySystem.BLL;
    using LotterySystem.Common;
    using System;

    public static class CallBLL
    {
        public static cz_adBLL cz_ad_bll;
        public static cz_admin_sysconfigBLL cz_admin_sysconfig_bll;
        public static cz_auto_downodds_log_sixBLL cz_auto_downodds_log_six_bll;
        public static cz_autosale_car168BLL cz_autosale_car168_bll;
        public static cz_autosale_cqscBLL cz_autosale_cqsc_bll;
        public static cz_autosale_happycarBLL cz_autosale_happycar_bll;
        public static cz_autosale_jscarBLL cz_autosale_jscar_bll;
        public static cz_autosale_jscqscBLL cz_autosale_jscqsc_bll;
        public static cz_autosale_jsft2BLL cz_autosale_jsft2_bll;
        public static cz_autosale_jsk3BLL cz_autosale_jsk3_bll;
        public static cz_autosale_jspk10BLL cz_autosale_jspk10_bll;
        public static cz_autosale_jssfcBLL cz_autosale_jssfc_bll;
        public static cz_autosale_k8scBLL cz_autosale_k8sc_bll;
        public static cz_autosale_kl10BLL cz_autosale_kl10_bll;
        public static cz_autosale_kl8BLL cz_autosale_kl8_bll;
        public static cz_autosale_pcddBLL cz_autosale_pcdd_bll;
        public static cz_autosale_pk10BLL cz_autosale_pk10_bll;
        public static cz_autosale_pkbjlBLL cz_autosale_pkbjl_bll;
        public static cz_autosale_sixBLL cz_autosale_six_bll;
        public static cz_autosale_speed5BLL cz_autosale_speed5_bll;
        public static cz_autosale_ssc168BLL cz_autosale_ssc168_bll;
        public static cz_autosale_vrcarBLL cz_autosale_vrcar_bll;
        public static cz_autosale_vrsscBLL cz_autosale_vrssc_bll;
        public static cz_autosale_xyft5BLL cz_autosale_xyft5_bll;
        public static cz_autosale_xyftoaBLL cz_autosale_xyftoa_bll;
        public static cz_autosale_xyftsgBLL cz_autosale_xyftsg_bll;
        public static cz_autosale_xyncBLL cz_autosale_xync_bll;
        public static cz_bet_kcBLL cz_bet_kc_bll;
        public static cz_bet_sixBLL cz_bet_six_bll;
        public static cz_betold_kcBLL cz_betold_kc_bll;
        public static cz_betold_sixBLL cz_betold_six_bll;
        public static cz_credit_lockBLL cz_credit_lock_bll;
        public static cz_dlsale_logBLL cz_dlsale_log_bll;
        public static cz_downodds_set_kcBLL cz_downodds_set_kc_bll;
        public static cz_drawback_car168BLL cz_drawback_car168_bll;
        public static cz_drawback_car168_tplBLL cz_drawback_car168_tpl_bll;
        public static cz_drawback_cqscBLL cz_drawback_cqsc_bll;
        public static cz_drawback_cqsc_tplBLL cz_drawback_cqsc_tpl_bll;
        public static cz_drawback_happycarBLL cz_drawback_happycar_bll;
        public static cz_drawback_happycar_tplBLL cz_drawback_happycar_tpl_bll;
        public static cz_drawback_jscarBLL cz_drawback_jscar_bll;
        public static cz_drawback_jscar_tplBLL cz_drawback_jscar_tpl_bll;
        public static cz_drawback_jscqscBLL cz_drawback_jscqsc_bll;
        public static cz_drawback_jscqsc_tplBLL cz_drawback_jscqsc_tpl_bll;
        public static cz_drawback_jsft2BLL cz_drawback_jsft2_bll;
        public static cz_drawback_jsft2_tplBLL cz_drawback_jsft2_tpl_bll;
        public static cz_drawback_jsk3BLL cz_drawback_jsk3_bll;
        public static cz_drawback_jsk3_tplBLL cz_drawback_jsk3_tpl_bll;
        public static cz_drawback_jspk10BLL cz_drawback_jspk10_bll;
        public static cz_drawback_jspk10_tplBLL cz_drawback_jspk10_tpl_bll;
        public static cz_drawback_jssfcBLL cz_drawback_jssfc_bll;
        public static cz_drawback_jssfc_tplBLL cz_drawback_jssfc_tpl_bll;
        public static cz_drawback_k8scBLL cz_drawback_k8sc_bll;
        public static cz_drawback_k8sc_tplBLL cz_drawback_k8sc_tpl_bll;
        public static cz_drawback_kl10BLL cz_drawback_kl10_bll;
        public static cz_drawback_kl10_tplBLL cz_drawback_kl10_tpl_bll;
        public static cz_drawback_kl8BLL cz_drawback_kl8_bll;
        public static cz_drawback_kl8_tplBLL cz_drawback_kl8_tpl_bll;
        public static cz_drawback_pcddBLL cz_drawback_pcdd_bll;
        public static cz_drawback_pcdd_tplBLL cz_drawback_pcdd_tpl_bll;
        public static cz_drawback_pk10BLL cz_drawback_pk10_bll;
        public static cz_drawback_pk10_tplBLL cz_drawback_pk10_tpl_bll;
        public static cz_drawback_pkbjlBLL cz_drawback_pkbjl_bll;
        public static cz_drawback_pkbjl_tplBLL cz_drawback_pkbjl_tpl_bll;
        public static cz_drawback_sixBLL cz_drawback_six_bll;
        public static cz_drawback_six_tplBLL cz_drawback_six_tpl_bll;
        public static cz_drawback_speed5BLL cz_drawback_speed5_bll;
        public static cz_drawback_ssc168BLL cz_drawback_ssc168_bll;
        public static cz_drawback_ssc168_tplBLL cz_drawback_ssc168_tpl_bll;
        public static cz_drawback_vrcarBLL cz_drawback_vrcar_bll;
        public static cz_drawback_vrcar_tplBLL cz_drawback_vrcar_tpl_bll;
        public static cz_drawback_vrsscBLL cz_drawback_vrssc_bll;
        public static cz_drawback_vrssc_tplBLL cz_drawback_vrssc_tpl_bll;
        public static cz_drawback_xyft5BLL cz_drawback_xyft5_bll;
        public static cz_drawback_xyft5_tplBLL cz_drawback_xyft5_tpl_bll;
        public static cz_drawback_xyftoaBLL cz_drawback_xyftoa_bll;
        public static cz_drawback_xyftoa_tplBLL cz_drawback_xyftoa_tpl_bll;
        public static cz_drawback_xyftsgBLL cz_drawback_xyftsg_bll;
        public static cz_drawback_xyftsg_tplBLL cz_drawback_xyftsg_tpl_bll;
        public static cz_drawback_xyncBLL cz_drawback_xync_bll;
        public static cz_drawback_xync_tplBLL cz_drawback_xync_tpl_bll;
        public static cz_five_not_in_sixBLL cz_five_not_in_six_bll;
        public static cz_jp_oddsBLL cz_jp_odds_bll;
        public static cz_lm_group_setBLL cz_lm_group_set_bll;
        public static cz_lm_number_amount_sixBLL cz_lm_number_amount_six_bll;
        public static cz_login_logBLL cz_login_log_bll;
        public static cz_lotteryBLL cz_lottery_bll;
        public static cz_mantissa_sixBLL cz_mantissa_six_bll;
        public static cz_odds_car168BLL cz_odds_car168_bll;
        public static cz_odds_cqscBLL cz_odds_cqsc_bll;
        public static cz_odds_happycarBLL cz_odds_happycar_bll;
        public static cz_odds_jscarBLL cz_odds_jscar_bll;
        public static cz_odds_jscqscBLL cz_odds_jscqsc_bll;
        public static cz_odds_jsft2BLL cz_odds_jsft2_bll;
        public static cz_odds_jsk3BLL cz_odds_jsk3_bll;
        public static cz_odds_jspk10BLL cz_odds_jspk10_bll;
        public static cz_odds_jssfcBLL cz_odds_jssfc_bll;
        public static cz_odds_k8scBLL cz_odds_k8sc_bll;
        public static cz_odds_kl10BLL cz_odds_kl10_bll;
        public static cz_odds_kl8BLL cz_odds_kl8_bll;
        public static cz_odds_pcddBLL cz_odds_pcdd_bll;
        public static cz_odds_pk10BLL cz_odds_pk10_bll;
        public static cz_odds_pkbjlBLL cz_odds_pkbjl_bll;
        public static cz_odds_sixBLL cz_odds_six_bll;
        public static cz_odds_speed5BLL cz_odds_speed5_bll;
        public static cz_odds_ssc168BLL cz_odds_ssc168_bll;
        public static cz_odds_vrcarBLL cz_odds_vrcar_bll;
        public static cz_odds_vrsscBLL cz_odds_vrssc_bll;
        public static cz_odds_wt_car168BLL cz_odds_wt_car168_bll;
        public static cz_odds_wt_cqscBLL cz_odds_wt_cqsc_bll;
        public static cz_odds_wt_happycarBLL cz_odds_wt_happycar_bll;
        public static cz_odds_wt_jscarBLL cz_odds_wt_jscar_bll;
        public static cz_odds_wt_jscqscBLL cz_odds_wt_jscqsc_bll;
        public static cz_odds_wt_jsft2BLL cz_odds_wt_jsft2_bll;
        public static cz_odds_wt_jsk3BLL cz_odds_wt_jsk3_bll;
        public static cz_odds_wt_jspk10BLL cz_odds_wt_jspk10_bll;
        public static cz_odds_wt_jssfcBLL cz_odds_wt_jssfc_bll;
        public static cz_odds_wt_k8scBLL cz_odds_wt_k8sc_bll;
        public static cz_odds_wt_kl10BLL cz_odds_wt_kl10_bll;
        public static cz_odds_wt_kl8BLL cz_odds_wt_kl8_bll;
        public static cz_odds_wt_pcddBLL cz_odds_wt_pcdd_bll;
        public static cz_odds_wt_pk10BLL cz_odds_wt_pk10_bll;
        public static cz_odds_wt_pkbjlBLL cz_odds_wt_pkbjl_bll;
        public static cz_odds_wt_sixBLL cz_odds_wt_six_bll;
        public static cz_odds_wt_speed5BLL cz_odds_wt_speed5_bll;
        public static cz_odds_wt_ssc168BLL cz_odds_wt_ssc168_bll;
        public static cz_odds_wt_vrcarBLL cz_odds_wt_vrcar_bll;
        public static cz_odds_wt_vrsscBLL cz_odds_wt_vrssc_bll;
        public static cz_odds_wt_xyft5BLL cz_odds_wt_xyft5_bll;
        public static cz_odds_wt_xyftoaBLL cz_odds_wt_xyftoa_bll;
        public static cz_odds_wt_xyftsgBLL cz_odds_wt_xyftsg_bll;
        public static cz_odds_wt_xyncBLL cz_odds_wt_xync_bll;
        public static cz_odds_xyft5BLL cz_odds_xyft5_bll;
        public static cz_odds_xyftoaBLL cz_odds_xyftoa_bll;
        public static cz_odds_xyftsgBLL cz_odds_xyftsg_bll;
        public static cz_odds_xyncBLL cz_odds_xync_bll;
        public static cz_opendataBLL cz_opendata_bll;
        public static cz_permissionsBLL cz_permissions_bll;
        public static cz_phase_car168BLL cz_phase_car168_bll;
        public static cz_phase_cqscBLL cz_phase_cqsc_bll;
        public static cz_phase_happycarBLL cz_phase_happycar_bll;
        public static cz_phase_jscarBLL cz_phase_jscar_bll;
        public static cz_phase_jscqscBLL cz_phase_jscqsc_bll;
        public static cz_phase_jsft2BLL cz_phase_jsft2_bll;
        public static cz_phase_jsk3BLL cz_phase_jsk3_bll;
        public static cz_phase_jspk10BLL cz_phase_jspk10_bll;
        public static cz_phase_jssfcBLL cz_phase_jssfc_bll;
        public static cz_phase_k8scBLL cz_phase_k8sc_bll;
        public static cz_phase_kl10BLL cz_phase_kl10_bll;
        public static cz_phase_kl8BLL cz_phase_kl8_bll;
        public static cz_phase_pcddBLL cz_phase_pcdd_bll;
        public static cz_phase_pk10BLL cz_phase_pk10_bll;
        public static cz_phase_pkbjlBLL cz_phase_pkbjl_bll;
        public static cz_phase_sixBLL cz_phase_six_bll;
        public static cz_phase_speed5BLL cz_phase_speed5_bll;
        public static cz_phase_ssc168BLL cz_phase_ssc168_bll;
        public static cz_phase_vrcarBLL cz_phase_vrcar_bll;
        public static cz_phase_vrsscBLL cz_phase_vrssc_bll;
        public static cz_phase_xyft5BLL cz_phase_xyft5_bll;
        public static cz_phase_xyftoaBLL cz_phase_xyftoa_bll;
        public static cz_phase_xyftsgBLL cz_phase_xyftsg_bll;
        public static cz_phase_xyncBLL cz_phase_xync_bll;
        public static cz_play_car168BLL cz_play_car168_bll;
        public static cz_play_cqscBLL cz_play_cqsc_bll;
        public static cz_play_happycarBLL cz_play_happycar_bll;
        public static cz_play_jscarBLL cz_play_jscar_bll;
        public static cz_play_jscqscBLL cz_play_jscqsc_bll;
        public static cz_play_jsft2BLL cz_play_jsft2_bll;
        public static cz_play_jsk3BLL cz_play_jsk3_bll;
        public static cz_play_jspk10BLL cz_play_jspk10_bll;
        public static cz_play_jssfcBLL cz_play_jssfc_bll;
        public static cz_play_k8scBLL cz_play_k8sc_bll;
        public static cz_play_kl10BLL cz_play_kl10_bll;
        public static cz_play_kl8BLL cz_play_kl8_bll;
        public static cz_play_pcddBLL cz_play_pcdd_bll;
        public static cz_play_pk10BLL cz_play_pk10_bll;
        public static cz_play_pkbjlBLL cz_play_pkbjl_bll;
        public static cz_play_sixBLL cz_play_six_bll;
        public static cz_play_speed5BLL cz_play_speed5_bll;
        public static cz_play_ssc168BLL cz_play_ssc168_bll;
        public static cz_play_vrcarBLL cz_play_vrcar_bll;
        public static cz_play_vrsscBLL cz_play_vrssc_bll;
        public static cz_play_xyft5BLL cz_play_xyft5_bll;
        public static cz_play_xyftoaBLL cz_play_xyftoa_bll;
        public static cz_play_xyftsgBLL cz_play_xyftsg_bll;
        public static cz_play_xyncBLL cz_play_xync_bll;
        public static cz_poker_pkbjlBLL cz_poker_pkbjl_bll;
        public static cz_rate_kcBLL cz_rate_kc_bll;
        public static cz_rate_sixBLL cz_rate_six_bll;
        public static cz_saleset_sixBLL cz_saleset_six_bll;
        public static cz_splitgroup_jssfcBLL cz_splitgroup_jssfc_bll;
        public static cz_splitgroup_kl10BLL cz_splitgroup_kl10_bll;
        public static cz_splitgroup_pcddBLL cz_splitgroup_pcdd_bll;
        public static cz_splitgroup_sixBLL cz_splitgroup_six_bll;
        public static cz_splitgroup_xyncBLL cz_splitgroup_xync_bll;
        public static cz_stat_onlineBLL cz_stat_online_bll;
        public static cz_stat_top_onlineBLL cz_stat_top_online_bll;
        public static cz_system_logBLL cz_system_log_bll;
        public static cz_system_set_car168BLL cz_system_set_car168_bll;
        public static cz_system_set_cqscBLL cz_system_set_cqsc_bll;
        public static cz_system_set_happycarBLL cz_system_set_happycar_bll;
        public static cz_system_set_jscarBLL cz_system_set_jscar_bll;
        public static cz_system_set_jscqscBLL cz_system_set_jscqsc_bll;
        public static cz_system_set_jsft2BLL cz_system_set_jsft2_bll;
        public static cz_system_set_jsk3BLL cz_system_set_jsk3_bll;
        public static cz_system_set_jspk10BLL cz_system_set_jspk10_bll;
        public static cz_system_set_jssfcBLL cz_system_set_jssfc_bll;
        public static cz_system_set_k8scBLL cz_system_set_k8sc_bll;
        public static cz_system_set_kcBLL cz_system_set_kc_bll;
        public static cz_system_set_kl10BLL cz_system_set_kl10_bll;
        public static cz_system_set_kl8BLL cz_system_set_kl8_bll;
        public static cz_system_set_pk10BLL cz_system_set_pk10_bll;
        public static cz_system_set_pkbjlBLL cz_system_set_pkbjl_bll;
        public static cz_system_set_sixBLL cz_system_set_six_bll;
        public static cz_system_set_ssc168BLL cz_system_set_ssc168_bll;
        public static cz_system_set_vrcarBLL cz_system_set_vrcar_bll;
        public static cz_system_set_vrsscBLL cz_system_set_vrssc_bll;
        public static cz_system_set_xyft5BLL cz_system_set_xyft5_bll;
        public static cz_system_set_xyftoaBLL cz_system_set_xyftoa_bll;
        public static cz_system_set_xyftsgBLL cz_system_set_xyftsg_bll;
        public static cz_system_set_xyncBLL cz_system_set_xync_bll;
        public static cz_szywt_jssfcBLL cz_szywt_jssfc_bll;
        public static cz_szywt_kl10BLL cz_szywt_kl10_bll;
        public static cz_szywt_sixBLL cz_szywt_six_bll;
        public static cz_szywt_xyncBLL cz_szywt_xync_bll;
        public static cz_three_win_sixBLL cz_three_win_six_bll;
        public static cz_user_change_logBLL cz_user_change_log_bll;
        public static cz_user_psw_err_logBLL cz_user_psw_err_log_bll;
        public static cz_usersBLL cz_users_bll;
        public static cz_users_childBLL cz_users_child_bll;
        public static cz_wt_10bz_sixBLL cz_wt_10bz_six_bll;
        public static cz_wt_2qz_b_sixBLL cz_wt_2qz_b_six_bll;
        public static cz_wt_2qz_sixBLL cz_wt_2qz_six_bll;
        public static cz_wt_2zt_b_sixBLL cz_wt_2zt_b_six_bll;
        public static cz_wt_2zt_sixBLL cz_wt_2zt_six_bll;
        public static cz_wt_3qz_b_sixBLL cz_wt_3qz_b_six_bll;
        public static cz_wt_3qz_sixBLL cz_wt_3qz_six_bll;
        public static cz_wt_3z2_b_sixBLL cz_wt_3z2_b_six_bll;
        public static cz_wt_3z2_sixBLL cz_wt_3z2_six_bll;
        public static cz_wt_4z1_b_sixBLL cz_wt_4z1_b_six_bll;
        public static cz_wt_4z1_sixBLL cz_wt_4z1_six_bll;
        public static cz_wt_5bz_sixBLL cz_wt_5bz_six_bll;
        public static cz_wt_6bz_sixBLL cz_wt_6bz_six_bll;
        public static cz_wt_6xyz_sixBLL cz_wt_6xyz_six_bll;
        public static cz_wt_7bz_sixBLL cz_wt_7bz_six_bll;
        public static cz_wt_8bz_sixBLL cz_wt_8bz_six_bll;
        public static cz_wt_9bz_sixBLL cz_wt_9bz_six_bll;
        public static cz_wt_jssfcBLL cz_wt_jssfc_bll;
        public static cz_wt_kl10BLL cz_wt_kl10_bll;
        public static cz_wt_pcddBLL cz_wt_pcdd_bll;
        public static cz_wt_sxlwsl_sixBLL cz_wt_sxlwsl_six_bll;
        public static cz_wt_tc_b_sixBLL cz_wt_tc_b_six_bll;
        public static cz_wt_tc_sixBLL cz_wt_tc_six_bll;
        public static cz_wt_xyncBLL cz_wt_xync_bll;
        public static cz_zodiac_old_sixBLL cz_zodiac_old_six_bll;
        public static cz_zodiac_sixBLL cz_zodiac_six_bll;
        public static cz_zodiac_six_wtBLL cz_zodiac_six_wt_bll;
        public static RedisHelper redisHelper = null;

        public static void Call()
        {
            if (cz_credit_lock_bll == null)
            {
                cz_credit_lock_bll = new cz_credit_lockBLL();
            }
            if (cz_user_psw_err_log_bll == null)
            {
                cz_user_psw_err_log_bll = new cz_user_psw_err_logBLL();
            }
            if (cz_stat_top_online_bll == null)
            {
                cz_stat_top_online_bll = new cz_stat_top_onlineBLL();
            }
            if (cz_betold_kc_bll == null)
            {
                cz_betold_kc_bll = new cz_betold_kcBLL();
            }
            if (cz_betold_six_bll == null)
            {
                cz_betold_six_bll = new cz_betold_sixBLL();
            }
            if (cz_rate_kc_bll == null)
            {
                cz_rate_kc_bll = new cz_rate_kcBLL();
            }
            if (cz_splitgroup_kl10_bll == null)
            {
                cz_splitgroup_kl10_bll = new cz_splitgroup_kl10BLL();
            }
            if (cz_splitgroup_xync_bll == null)
            {
                cz_splitgroup_xync_bll = new cz_splitgroup_xyncBLL();
            }
            if (cz_splitgroup_pcdd_bll == null)
            {
                cz_splitgroup_pcdd_bll = new cz_splitgroup_pcddBLL();
            }
            if (cz_splitgroup_jssfc_bll == null)
            {
                cz_splitgroup_jssfc_bll = new cz_splitgroup_jssfcBLL();
            }
            if (cz_phase_cqsc_bll == null)
            {
                cz_phase_cqsc_bll = new cz_phase_cqscBLL();
            }
            if (cz_phase_jsk3_bll == null)
            {
                cz_phase_jsk3_bll = new cz_phase_jsk3BLL();
            }
            if (cz_phase_kl8_bll == null)
            {
                cz_phase_kl8_bll = new cz_phase_kl8BLL();
            }
            if (cz_phase_pk10_bll == null)
            {
                cz_phase_pk10_bll = new cz_phase_pk10BLL();
            }
            if (cz_phase_k8sc_bll == null)
            {
                cz_phase_k8sc_bll = new cz_phase_k8scBLL();
            }
            if (cz_phase_pcdd_bll == null)
            {
                cz_phase_pcdd_bll = new cz_phase_pcddBLL();
            }
            if (cz_phase_xyft5_bll == null)
            {
                cz_phase_xyft5_bll = new cz_phase_xyft5BLL();
            }
            if (cz_phase_pkbjl_bll == null)
            {
                cz_phase_pkbjl_bll = new cz_phase_pkbjlBLL();
            }
            if (cz_poker_pkbjl_bll == null)
            {
                cz_poker_pkbjl_bll = new cz_poker_pkbjlBLL();
            }
            if (cz_phase_jscar_bll == null)
            {
                cz_phase_jscar_bll = new cz_phase_jscarBLL();
            }
            if (cz_phase_speed5_bll == null)
            {
                cz_phase_speed5_bll = new cz_phase_speed5BLL();
            }
            if (cz_phase_jspk10_bll == null)
            {
                cz_phase_jspk10_bll = new cz_phase_jspk10BLL();
            }
            if (cz_phase_jscqsc_bll == null)
            {
                cz_phase_jscqsc_bll = new cz_phase_jscqscBLL();
            }
            if (cz_phase_jsft2_bll == null)
            {
                cz_phase_jsft2_bll = new cz_phase_jsft2BLL();
            }
            if (cz_phase_car168_bll == null)
            {
                cz_phase_car168_bll = new cz_phase_car168BLL();
            }
            if (cz_phase_ssc168_bll == null)
            {
                cz_phase_ssc168_bll = new cz_phase_ssc168BLL();
            }
            if (cz_phase_vrcar_bll == null)
            {
                cz_phase_vrcar_bll = new cz_phase_vrcarBLL();
            }
            if (cz_phase_vrssc_bll == null)
            {
                cz_phase_vrssc_bll = new cz_phase_vrsscBLL();
            }
            if (cz_phase_xyftoa_bll == null)
            {
                cz_phase_xyftoa_bll = new cz_phase_xyftoaBLL();
            }
            if (cz_phase_xyftsg_bll == null)
            {
                cz_phase_xyftsg_bll = new cz_phase_xyftsgBLL();
            }
            if (cz_phase_happycar_bll == null)
            {
                cz_phase_happycar_bll = new cz_phase_happycarBLL();
            }
            if (cz_users_bll == null)
            {
                cz_users_bll = new cz_usersBLL();
            }
            if (cz_phase_xync_bll == null)
            {
                cz_phase_xync_bll = new cz_phase_xyncBLL();
            }
            if (cz_autosale_six_bll == null)
            {
                cz_autosale_six_bll = new cz_autosale_sixBLL();
            }
            if (cz_play_cqsc_bll == null)
            {
                cz_play_cqsc_bll = new cz_play_cqscBLL();
            }
            if (cz_play_k8sc_bll == null)
            {
                cz_play_k8sc_bll = new cz_play_k8scBLL();
            }
            if (cz_play_pcdd_bll == null)
            {
                cz_play_pcdd_bll = new cz_play_pcddBLL();
            }
            if (cz_play_xyft5_bll == null)
            {
                cz_play_xyft5_bll = new cz_play_xyft5BLL();
            }
            if (cz_play_jsk3_bll == null)
            {
                cz_play_jsk3_bll = new cz_play_jsk3BLL();
            }
            if (cz_bet_six_bll == null)
            {
                cz_bet_six_bll = new cz_bet_sixBLL();
            }
            if (cz_play_kl10_bll == null)
            {
                cz_play_kl10_bll = new cz_play_kl10BLL();
            }
            if (cz_play_jssfc_bll == null)
            {
                cz_play_jssfc_bll = new cz_play_jssfcBLL();
            }
            if (cz_saleset_six_bll == null)
            {
                cz_saleset_six_bll = new cz_saleset_sixBLL();
            }
            if (cz_play_kl8_bll == null)
            {
                cz_play_kl8_bll = new cz_play_kl8BLL();
            }
            if (cz_rate_six_bll == null)
            {
                cz_rate_six_bll = new cz_rate_sixBLL();
            }
            if (cz_play_pk10_bll == null)
            {
                cz_play_pk10_bll = new cz_play_pk10BLL();
            }
            if (cz_dlsale_log_bll == null)
            {
                cz_dlsale_log_bll = new cz_dlsale_logBLL();
            }
            if (cz_play_xync_bll == null)
            {
                cz_play_xync_bll = new cz_play_xyncBLL();
            }
            if (cz_play_pkbjl_bll == null)
            {
                cz_play_pkbjl_bll = new cz_play_pkbjlBLL();
            }
            if (cz_play_jscar_bll == null)
            {
                cz_play_jscar_bll = new cz_play_jscarBLL();
            }
            if (cz_play_speed5_bll == null)
            {
                cz_play_speed5_bll = new cz_play_speed5BLL();
            }
            if (cz_play_jspk10_bll == null)
            {
                cz_play_jspk10_bll = new cz_play_jspk10BLL();
            }
            if (cz_play_jscqsc_bll == null)
            {
                cz_play_jscqsc_bll = new cz_play_jscqscBLL();
            }
            if (cz_play_jsft2_bll == null)
            {
                cz_play_jsft2_bll = new cz_play_jsft2BLL();
            }
            if (cz_play_car168_bll == null)
            {
                cz_play_car168_bll = new cz_play_car168BLL();
            }
            if (cz_play_ssc168_bll == null)
            {
                cz_play_ssc168_bll = new cz_play_ssc168BLL();
            }
            if (cz_play_vrcar_bll == null)
            {
                cz_play_vrcar_bll = new cz_play_vrcarBLL();
            }
            if (cz_play_vrssc_bll == null)
            {
                cz_play_vrssc_bll = new cz_play_vrsscBLL();
            }
            if (cz_play_xyftoa_bll == null)
            {
                cz_play_xyftoa_bll = new cz_play_xyftoaBLL();
            }
            if (cz_play_xyftsg_bll == null)
            {
                cz_play_xyftsg_bll = new cz_play_xyftsgBLL();
            }
            if (cz_play_happycar_bll == null)
            {
                cz_play_happycar_bll = new cz_play_happycarBLL();
            }
            if (cz_downodds_set_kc_bll == null)
            {
                cz_downodds_set_kc_bll = new cz_downodds_set_kcBLL();
            }
            if (cz_user_change_log_bll == null)
            {
                cz_user_change_log_bll = new cz_user_change_logBLL();
            }
            if (cz_odds_cqsc_bll == null)
            {
                cz_odds_cqsc_bll = new cz_odds_cqscBLL();
            }
            if (cz_odds_speed5_bll == null)
            {
                cz_odds_speed5_bll = new cz_odds_speed5BLL();
            }
            if (cz_odds_k8sc_bll == null)
            {
                cz_odds_k8sc_bll = new cz_odds_k8scBLL();
            }
            if (cz_odds_jsk3_bll == null)
            {
                cz_odds_jsk3_bll = new cz_odds_jsk3BLL();
            }
            if (cz_odds_pkbjl_bll == null)
            {
                cz_odds_pkbjl_bll = new cz_odds_pkbjlBLL();
            }
            if (cz_odds_kl10_bll == null)
            {
                cz_odds_kl10_bll = new cz_odds_kl10BLL();
            }
            if (cz_odds_jssfc_bll == null)
            {
                cz_odds_jssfc_bll = new cz_odds_jssfcBLL();
            }
            if (cz_odds_kl8_bll == null)
            {
                cz_odds_kl8_bll = new cz_odds_kl8BLL();
            }
            if (cz_odds_pk10_bll == null)
            {
                cz_odds_pk10_bll = new cz_odds_pk10BLL();
            }
            if (cz_odds_xync_bll == null)
            {
                cz_odds_xync_bll = new cz_odds_xyncBLL();
            }
            if (cz_odds_pcdd_bll == null)
            {
                cz_odds_pcdd_bll = new cz_odds_pcddBLL();
            }
            if (cz_odds_xyft5_bll == null)
            {
                cz_odds_xyft5_bll = new cz_odds_xyft5BLL();
            }
            if (cz_odds_jscar_bll == null)
            {
                cz_odds_jscar_bll = new cz_odds_jscarBLL();
            }
            if (cz_odds_jspk10_bll == null)
            {
                cz_odds_jspk10_bll = new cz_odds_jspk10BLL();
            }
            if (cz_odds_jscqsc_bll == null)
            {
                cz_odds_jscqsc_bll = new cz_odds_jscqscBLL();
            }
            if (cz_odds_jsft2_bll == null)
            {
                cz_odds_jsft2_bll = new cz_odds_jsft2BLL();
            }
            if (cz_odds_car168_bll == null)
            {
                cz_odds_car168_bll = new cz_odds_car168BLL();
            }
            if (cz_odds_ssc168_bll == null)
            {
                cz_odds_ssc168_bll = new cz_odds_ssc168BLL();
            }
            if (cz_odds_vrcar_bll == null)
            {
                cz_odds_vrcar_bll = new cz_odds_vrcarBLL();
            }
            if (cz_odds_vrssc_bll == null)
            {
                cz_odds_vrssc_bll = new cz_odds_vrsscBLL();
            }
            if (cz_odds_xyftoa_bll == null)
            {
                cz_odds_xyftoa_bll = new cz_odds_xyftoaBLL();
            }
            if (cz_odds_xyftsg_bll == null)
            {
                cz_odds_xyftsg_bll = new cz_odds_xyftsgBLL();
            }
            if (cz_odds_happycar_bll == null)
            {
                cz_odds_happycar_bll = new cz_odds_happycarBLL();
            }
            if (cz_splitgroup_six_bll == null)
            {
                cz_splitgroup_six_bll = new cz_splitgroup_sixBLL();
            }
            if (cz_opendata_bll == null)
            {
                cz_opendata_bll = new cz_opendataBLL();
            }
            if (cz_drawback_cqsc_bll == null)
            {
                cz_drawback_cqsc_bll = new cz_drawback_cqscBLL();
            }
            if (cz_drawback_k8sc_bll == null)
            {
                cz_drawback_k8sc_bll = new cz_drawback_k8scBLL();
            }
            if (cz_drawback_pcdd_bll == null)
            {
                cz_drawback_pcdd_bll = new cz_drawback_pcddBLL();
            }
            if (cz_drawback_xyft5_bll == null)
            {
                cz_drawback_xyft5_bll = new cz_drawback_xyft5BLL();
            }
            if (cz_drawback_jsk3_bll == null)
            {
                cz_drawback_jsk3_bll = new cz_drawback_jsk3BLL();
            }
            if (cz_drawback_kl10_bll == null)
            {
                cz_drawback_kl10_bll = new cz_drawback_kl10BLL();
            }
            if (cz_drawback_jssfc_bll == null)
            {
                cz_drawback_jssfc_bll = new cz_drawback_jssfcBLL();
            }
            if (cz_drawback_pkbjl_bll == null)
            {
                cz_drawback_pkbjl_bll = new cz_drawback_pkbjlBLL();
            }
            if (cz_drawback_jscar_bll == null)
            {
                cz_drawback_jscar_bll = new cz_drawback_jscarBLL();
            }
            if (cz_drawback_speed5_bll == null)
            {
                cz_drawback_speed5_bll = new cz_drawback_speed5BLL();
            }
            if (cz_drawback_car168_bll == null)
            {
                cz_drawback_car168_bll = new cz_drawback_car168BLL();
            }
            if (cz_drawback_ssc168_bll == null)
            {
                cz_drawback_ssc168_bll = new cz_drawback_ssc168BLL();
            }
            if (cz_drawback_vrcar_bll == null)
            {
                cz_drawback_vrcar_bll = new cz_drawback_vrcarBLL();
            }
            if (cz_drawback_vrssc_bll == null)
            {
                cz_drawback_vrssc_bll = new cz_drawback_vrsscBLL();
            }
            if (cz_drawback_xyftoa_bll == null)
            {
                cz_drawback_xyftoa_bll = new cz_drawback_xyftoaBLL();
            }
            if (cz_drawback_xyftsg_bll == null)
            {
                cz_drawback_xyftsg_bll = new cz_drawback_xyftsgBLL();
            }
            if (cz_drawback_happycar_bll == null)
            {
                cz_drawback_happycar_bll = new cz_drawback_happycarBLL();
            }
            if (cz_auto_downodds_log_six_bll == null)
            {
                cz_auto_downodds_log_six_bll = new cz_auto_downodds_log_sixBLL();
            }
            if (cz_drawback_kl8_bll == null)
            {
                cz_drawback_kl8_bll = new cz_drawback_kl8BLL();
            }
            if (cz_phase_six_bll == null)
            {
                cz_phase_six_bll = new cz_phase_sixBLL();
            }
            if (cz_drawback_pk10_bll == null)
            {
                cz_drawback_pk10_bll = new cz_drawback_pk10BLL();
            }
            if (cz_drawback_jspk10_bll == null)
            {
                cz_drawback_jspk10_bll = new cz_drawback_jspk10BLL();
            }
            if (cz_drawback_jsft2_bll == null)
            {
                cz_drawback_jsft2_bll = new cz_drawback_jsft2BLL();
            }
            if (cz_drawback_jscqsc_bll == null)
            {
                cz_drawback_jscqsc_bll = new cz_drawback_jscqscBLL();
            }
            if (cz_phase_jssfc_bll == null)
            {
                cz_phase_jssfc_bll = new cz_phase_jssfcBLL();
            }
            if (cz_phase_kl10_bll == null)
            {
                cz_phase_kl10_bll = new cz_phase_kl10BLL();
            }
            if (cz_drawback_xync_bll == null)
            {
                cz_drawback_xync_bll = new cz_drawback_xyncBLL();
            }
            if (cz_drawback_xyft5_bll == null)
            {
                cz_drawback_xyft5_bll = new cz_drawback_xyft5BLL();
            }
            if (cz_login_log_bll == null)
            {
                cz_login_log_bll = new cz_login_logBLL();
            }
            if (cz_drawback_six_tpl_bll == null)
            {
                cz_drawback_six_tpl_bll = new cz_drawback_six_tplBLL();
            }
            if (cz_play_six_bll == null)
            {
                cz_play_six_bll = new cz_play_sixBLL();
            }
            if (cz_drawback_cqsc_tpl_bll == null)
            {
                cz_drawback_cqsc_tpl_bll = new cz_drawback_cqsc_tplBLL();
            }
            if (cz_drawback_k8sc_tpl_bll == null)
            {
                cz_drawback_k8sc_tpl_bll = new cz_drawback_k8sc_tplBLL();
            }
            if (cz_drawback_jsk3_tpl_bll == null)
            {
                cz_drawback_jsk3_tpl_bll = new cz_drawback_jsk3_tplBLL();
            }
            if (cz_drawback_kl10_tpl_bll == null)
            {
                cz_drawback_kl10_tpl_bll = new cz_drawback_kl10_tplBLL();
            }
            if (cz_drawback_jssfc_tpl_bll == null)
            {
                cz_drawback_jssfc_tpl_bll = new cz_drawback_jssfc_tplBLL();
            }
            if (cz_drawback_kl8_tpl_bll == null)
            {
                cz_drawback_kl8_tpl_bll = new cz_drawback_kl8_tplBLL();
            }
            if (cz_drawback_pk10_tpl_bll == null)
            {
                cz_drawback_pk10_tpl_bll = new cz_drawback_pk10_tplBLL();
            }
            if (cz_drawback_jspk10_tpl_bll == null)
            {
                cz_drawback_jspk10_tpl_bll = new cz_drawback_jspk10_tplBLL();
            }
            if (cz_drawback_jsft2_tpl_bll == null)
            {
                cz_drawback_jsft2_tpl_bll = new cz_drawback_jsft2_tplBLL();
            }
            if (cz_drawback_jscqsc_tpl_bll == null)
            {
                cz_drawback_jscqsc_tpl_bll = new cz_drawback_jscqsc_tplBLL();
            }
            if (cz_drawback_xync_tpl_bll == null)
            {
                cz_drawback_xync_tpl_bll = new cz_drawback_xync_tplBLL();
            }
            if (cz_drawback_pcdd_tpl_bll == null)
            {
                cz_drawback_pcdd_tpl_bll = new cz_drawback_pcdd_tplBLL();
            }
            if (cz_drawback_xyft5_tpl_bll == null)
            {
                cz_drawback_xyft5_tpl_bll = new cz_drawback_xyft5_tplBLL();
            }
            if (cz_drawback_pkbjl_tpl_bll == null)
            {
                cz_drawback_pkbjl_tpl_bll = new cz_drawback_pkbjl_tplBLL();
            }
            if (cz_drawback_jscar_tpl_bll == null)
            {
                cz_drawback_jscar_tpl_bll = new cz_drawback_jscar_tplBLL();
            }
            if (cz_drawback_car168_tpl_bll == null)
            {
                cz_drawback_car168_tpl_bll = new cz_drawback_car168_tplBLL();
            }
            if (cz_drawback_ssc168_tpl_bll == null)
            {
                cz_drawback_ssc168_tpl_bll = new cz_drawback_ssc168_tplBLL();
            }
            if (cz_drawback_vrcar_tpl_bll == null)
            {
                cz_drawback_vrcar_tpl_bll = new cz_drawback_vrcar_tplBLL();
            }
            if (cz_drawback_xyftoa_tpl_bll == null)
            {
                cz_drawback_xyftoa_tpl_bll = new cz_drawback_xyftoa_tplBLL();
            }
            if (cz_drawback_xyftsg_tpl_bll == null)
            {
                cz_drawback_xyftsg_tpl_bll = new cz_drawback_xyftsg_tplBLL();
            }
            if (cz_drawback_happycar_tpl_bll == null)
            {
                cz_drawback_happycar_tpl_bll = new cz_drawback_happycar_tplBLL();
            }
            if (cz_odds_six_bll == null)
            {
                cz_odds_six_bll = new cz_odds_sixBLL();
            }
            if (cz_system_set_cqsc_bll == null)
            {
                cz_system_set_cqsc_bll = new cz_system_set_cqscBLL();
            }
            if (cz_system_set_k8sc_bll == null)
            {
                cz_system_set_k8sc_bll = new cz_system_set_k8scBLL();
            }
            if (cz_system_set_xyft5_bll == null)
            {
                cz_system_set_xyft5_bll = new cz_system_set_xyft5BLL();
            }
            if (cz_system_set_pkbjl_bll == null)
            {
                cz_system_set_pkbjl_bll = new cz_system_set_pkbjlBLL();
            }
            if (cz_system_set_jscar_bll == null)
            {
                cz_system_set_jscar_bll = new cz_system_set_jscarBLL();
            }
            if (cz_stat_online_bll == null)
            {
                cz_stat_online_bll = new cz_stat_onlineBLL();
            }
            if (cz_system_set_jsk3_bll == null)
            {
                cz_system_set_jsk3_bll = new cz_system_set_jsk3BLL();
            }
            if (cz_drawback_six_bll == null)
            {
                cz_drawback_six_bll = new cz_drawback_sixBLL();
            }
            if (cz_system_set_kl10_bll == null)
            {
                cz_system_set_kl10_bll = new cz_system_set_kl10BLL();
            }
            if (cz_system_set_jssfc_bll == null)
            {
                cz_system_set_jssfc_bll = new cz_system_set_jssfcBLL();
            }
            if (cz_system_set_kl8_bll == null)
            {
                cz_system_set_kl8_bll = new cz_system_set_kl8BLL();
            }
            if (cz_system_set_car168_bll == null)
            {
                cz_system_set_car168_bll = new cz_system_set_car168BLL();
            }
            if (cz_system_set_ssc168_bll == null)
            {
                cz_system_set_ssc168_bll = new cz_system_set_ssc168BLL();
            }
            if (cz_system_set_vrcar_bll == null)
            {
                cz_system_set_vrcar_bll = new cz_system_set_vrcarBLL();
            }
            if (cz_system_set_vrssc_bll == null)
            {
                cz_system_set_vrssc_bll = new cz_system_set_vrsscBLL();
            }
            if (cz_system_set_xyftoa_bll == null)
            {
                cz_system_set_xyftoa_bll = new cz_system_set_xyftoaBLL();
            }
            if (cz_system_set_xyftsg_bll == null)
            {
                cz_system_set_xyftsg_bll = new cz_system_set_xyftsgBLL();
            }
            if (cz_system_set_happycar_bll == null)
            {
                cz_system_set_happycar_bll = new cz_system_set_happycarBLL();
            }
            if (cz_permissions_bll == null)
            {
                cz_permissions_bll = new cz_permissionsBLL();
            }
            if (cz_system_set_pk10_bll == null)
            {
                cz_system_set_pk10_bll = new cz_system_set_pk10BLL();
            }
            if (cz_system_set_jspk10_bll == null)
            {
                cz_system_set_jspk10_bll = new cz_system_set_jspk10BLL();
            }
            if (cz_system_set_jsft2_bll == null)
            {
                cz_system_set_jsft2_bll = new cz_system_set_jsft2BLL();
            }
            if (cz_system_set_jscqsc_bll == null)
            {
                cz_system_set_jscqsc_bll = new cz_system_set_jscqscBLL();
            }
            if (cz_three_win_six_bll == null)
            {
                cz_three_win_six_bll = new cz_three_win_sixBLL();
            }
            if (cz_system_set_xync_bll == null)
            {
                cz_system_set_xync_bll = new cz_system_set_xyncBLL();
            }
            if (cz_users_child_bll == null)
            {
                cz_users_child_bll = new cz_users_childBLL();
            }
            if (cz_zodiac_six_bll == null)
            {
                cz_zodiac_six_bll = new cz_zodiac_sixBLL();
            }
            if (cz_zodiac_old_six_bll == null)
            {
                cz_zodiac_old_six_bll = new cz_zodiac_old_sixBLL();
            }
            if (cz_zodiac_six_wt_bll == null)
            {
                cz_zodiac_six_wt_bll = new cz_zodiac_six_wtBLL();
            }
            if (cz_system_set_six_bll == null)
            {
                cz_system_set_six_bll = new cz_system_set_sixBLL();
            }
            if (cz_system_set_kc_bll == null)
            {
                cz_system_set_kc_bll = new cz_system_set_kcBLL();
            }
            if (cz_szywt_six_bll == null)
            {
                cz_szywt_six_bll = new cz_szywt_sixBLL();
            }
            if (cz_szywt_kl10_bll == null)
            {
                cz_szywt_kl10_bll = new cz_szywt_kl10BLL();
            }
            if (cz_szywt_jssfc_bll == null)
            {
                cz_szywt_jssfc_bll = new cz_szywt_jssfcBLL();
            }
            if (cz_mantissa_six_bll == null)
            {
                cz_mantissa_six_bll = new cz_mantissa_sixBLL();
            }
            if (cz_szywt_xync_bll == null)
            {
                cz_szywt_xync_bll = new cz_szywt_xyncBLL();
            }
            if (cz_five_not_in_six_bll == null)
            {
                cz_five_not_in_six_bll = new cz_five_not_in_sixBLL();
            }
            if (cz_wt_kl10_bll == null)
            {
                cz_wt_kl10_bll = new cz_wt_kl10BLL();
            }
            if (cz_wt_jssfc_bll == null)
            {
                cz_wt_jssfc_bll = new cz_wt_jssfcBLL();
            }
            if (cz_wt_pcdd_bll == null)
            {
                cz_wt_pcdd_bll = new cz_wt_pcddBLL();
            }
            if (cz_wt_xync_bll == null)
            {
                cz_wt_xync_bll = new cz_wt_xyncBLL();
            }
            if (cz_lottery_bll == null)
            {
                cz_lottery_bll = new cz_lotteryBLL();
            }
            if (cz_ad_bll == null)
            {
                cz_ad_bll = new cz_adBLL();
            }
            if (cz_autosale_kl10_bll == null)
            {
                cz_autosale_kl10_bll = new cz_autosale_kl10BLL();
            }
            if (cz_autosale_jssfc_bll == null)
            {
                cz_autosale_jssfc_bll = new cz_autosale_jssfcBLL();
            }
            if (cz_autosale_cqsc_bll == null)
            {
                cz_autosale_cqsc_bll = new cz_autosale_cqscBLL();
            }
            if (cz_autosale_k8sc_bll == null)
            {
                cz_autosale_k8sc_bll = new cz_autosale_k8scBLL();
            }
            if (cz_autosale_kl8_bll == null)
            {
                cz_autosale_kl8_bll = new cz_autosale_kl8BLL();
            }
            if (cz_autosale_pk10_bll == null)
            {
                cz_autosale_pk10_bll = new cz_autosale_pk10BLL();
            }
            if (cz_autosale_jsk3_bll == null)
            {
                cz_autosale_jsk3_bll = new cz_autosale_jsk3BLL();
            }
            if (cz_autosale_xync_bll == null)
            {
                cz_autosale_xync_bll = new cz_autosale_xyncBLL();
            }
            if (cz_autosale_pcdd_bll == null)
            {
                cz_autosale_pcdd_bll = new cz_autosale_pcddBLL();
            }
            if (cz_autosale_xyft5_bll == null)
            {
                cz_autosale_xyft5_bll = new cz_autosale_xyft5BLL();
            }
            if (cz_autosale_pkbjl_bll == null)
            {
                cz_autosale_pkbjl_bll = new cz_autosale_pkbjlBLL();
            }
            if (cz_autosale_jscar_bll == null)
            {
                cz_autosale_jscar_bll = new cz_autosale_jscarBLL();
            }
            if (cz_autosale_speed5_bll == null)
            {
                cz_autosale_speed5_bll = new cz_autosale_speed5BLL();
            }
            if (cz_autosale_jspk10_bll == null)
            {
                cz_autosale_jspk10_bll = new cz_autosale_jspk10BLL();
            }
            if (cz_autosale_jsft2_bll == null)
            {
                cz_autosale_jsft2_bll = new cz_autosale_jsft2BLL();
            }
            if (cz_autosale_jscqsc_bll == null)
            {
                cz_autosale_jscqsc_bll = new cz_autosale_jscqscBLL();
            }
            if (cz_autosale_car168_bll == null)
            {
                cz_autosale_car168_bll = new cz_autosale_car168BLL();
            }
            if (cz_autosale_ssc168_bll == null)
            {
                cz_autosale_ssc168_bll = new cz_autosale_ssc168BLL();
            }
            if (cz_autosale_vrcar_bll == null)
            {
                cz_autosale_vrcar_bll = new cz_autosale_vrcarBLL();
            }
            if (cz_autosale_vrssc_bll == null)
            {
                cz_autosale_vrssc_bll = new cz_autosale_vrsscBLL();
            }
            if (cz_autosale_xyftoa_bll == null)
            {
                cz_autosale_xyftoa_bll = new cz_autosale_xyftoaBLL();
            }
            if (cz_autosale_xyftsg_bll == null)
            {
                cz_autosale_xyftsg_bll = new cz_autosale_xyftsgBLL();
            }
            if (cz_autosale_happycar_bll == null)
            {
                cz_autosale_happycar_bll = new cz_autosale_happycarBLL();
            }
            if (cz_bet_kc_bll == null)
            {
                cz_bet_kc_bll = new cz_bet_kcBLL();
            }
            if (cz_wt_3qz_six_bll == null)
            {
                cz_wt_3qz_six_bll = new cz_wt_3qz_sixBLL();
            }
            if (cz_wt_3z2_six_bll == null)
            {
                cz_wt_3z2_six_bll = new cz_wt_3z2_sixBLL();
            }
            if (cz_wt_2qz_six_bll == null)
            {
                cz_wt_2qz_six_bll = new cz_wt_2qz_sixBLL();
            }
            if (cz_wt_2zt_six_bll == null)
            {
                cz_wt_2zt_six_bll = new cz_wt_2zt_sixBLL();
            }
            if (cz_wt_tc_six_bll == null)
            {
                cz_wt_tc_six_bll = new cz_wt_tc_sixBLL();
            }
            if (cz_wt_4z1_six_bll == null)
            {
                cz_wt_4z1_six_bll = new cz_wt_4z1_sixBLL();
            }
            if (cz_wt_3qz_b_six_bll == null)
            {
                cz_wt_3qz_b_six_bll = new cz_wt_3qz_b_sixBLL();
            }
            if (cz_wt_3z2_b_six_bll == null)
            {
                cz_wt_3z2_b_six_bll = new cz_wt_3z2_b_sixBLL();
            }
            if (cz_wt_2qz_b_six_bll == null)
            {
                cz_wt_2qz_b_six_bll = new cz_wt_2qz_b_sixBLL();
            }
            if (cz_wt_2zt_b_six_bll == null)
            {
                cz_wt_2zt_b_six_bll = new cz_wt_2zt_b_sixBLL();
            }
            if (cz_wt_tc_b_six_bll == null)
            {
                cz_wt_tc_b_six_bll = new cz_wt_tc_b_sixBLL();
            }
            if (cz_wt_4z1_b_six_bll == null)
            {
                cz_wt_4z1_b_six_bll = new cz_wt_4z1_b_sixBLL();
            }
            if (cz_wt_5bz_six_bll == null)
            {
                cz_wt_5bz_six_bll = new cz_wt_5bz_sixBLL();
            }
            if (cz_wt_6bz_six_bll == null)
            {
                cz_wt_6bz_six_bll = new cz_wt_6bz_sixBLL();
            }
            if (cz_wt_7bz_six_bll == null)
            {
                cz_wt_7bz_six_bll = new cz_wt_7bz_sixBLL();
            }
            if (cz_wt_8bz_six_bll == null)
            {
                cz_wt_8bz_six_bll = new cz_wt_8bz_sixBLL();
            }
            if (cz_wt_9bz_six_bll == null)
            {
                cz_wt_9bz_six_bll = new cz_wt_9bz_sixBLL();
            }
            if (cz_wt_10bz_six_bll == null)
            {
                cz_wt_10bz_six_bll = new cz_wt_10bz_sixBLL();
            }
            if (cz_wt_sxlwsl_six_bll == null)
            {
                cz_wt_sxlwsl_six_bll = new cz_wt_sxlwsl_sixBLL();
            }
            if (cz_wt_6xyz_six_bll == null)
            {
                cz_wt_6xyz_six_bll = new cz_wt_6xyz_sixBLL();
            }
            if (cz_odds_wt_kl10_bll == null)
            {
                cz_odds_wt_kl10_bll = new cz_odds_wt_kl10BLL();
            }
            if (cz_odds_wt_jssfc_bll == null)
            {
                cz_odds_wt_jssfc_bll = new cz_odds_wt_jssfcBLL();
            }
            if (cz_odds_wt_cqsc_bll == null)
            {
                cz_odds_wt_cqsc_bll = new cz_odds_wt_cqscBLL();
            }
            if (cz_odds_wt_pk10_bll == null)
            {
                cz_odds_wt_pk10_bll = new cz_odds_wt_pk10BLL();
            }
            if (cz_odds_wt_xync_bll == null)
            {
                cz_odds_wt_xync_bll = new cz_odds_wt_xyncBLL();
            }
            if (cz_odds_wt_jsk3_bll == null)
            {
                cz_odds_wt_jsk3_bll = new cz_odds_wt_jsk3BLL();
            }
            if (cz_odds_wt_kl8_bll == null)
            {
                cz_odds_wt_kl8_bll = new cz_odds_wt_kl8BLL();
            }
            if (cz_odds_wt_six_bll == null)
            {
                cz_odds_wt_six_bll = new cz_odds_wt_sixBLL();
            }
            if (cz_odds_wt_k8sc_bll == null)
            {
                cz_odds_wt_k8sc_bll = new cz_odds_wt_k8scBLL();
            }
            if (cz_odds_wt_pcdd_bll == null)
            {
                cz_odds_wt_pcdd_bll = new cz_odds_wt_pcddBLL();
            }
            if (cz_odds_wt_xyft5_bll == null)
            {
                cz_odds_wt_xyft5_bll = new cz_odds_wt_xyft5BLL();
            }
            if (cz_odds_wt_pkbjl_bll == null)
            {
                cz_odds_wt_pkbjl_bll = new cz_odds_wt_pkbjlBLL();
            }
            if (cz_odds_wt_jscar_bll == null)
            {
                cz_odds_wt_jscar_bll = new cz_odds_wt_jscarBLL();
            }
            if (cz_odds_wt_speed5_bll == null)
            {
                cz_odds_wt_speed5_bll = new cz_odds_wt_speed5BLL();
            }
            if (cz_odds_wt_jspk10_bll == null)
            {
                cz_odds_wt_jspk10_bll = new cz_odds_wt_jspk10BLL();
            }
            if (cz_odds_wt_jscqsc_bll == null)
            {
                cz_odds_wt_jscqsc_bll = new cz_odds_wt_jscqscBLL();
            }
            if (cz_odds_wt_jsft2_bll == null)
            {
                cz_odds_wt_jsft2_bll = new cz_odds_wt_jsft2BLL();
            }
            if (cz_odds_wt_car168_bll == null)
            {
                cz_odds_wt_car168_bll = new cz_odds_wt_car168BLL();
            }
            if (cz_odds_wt_ssc168_bll == null)
            {
                cz_odds_wt_ssc168_bll = new cz_odds_wt_ssc168BLL();
            }
            if (cz_odds_wt_vrcar_bll == null)
            {
                cz_odds_wt_vrcar_bll = new cz_odds_wt_vrcarBLL();
            }
            if (cz_odds_wt_vrssc_bll == null)
            {
                cz_odds_wt_vrssc_bll = new cz_odds_wt_vrsscBLL();
            }
            if (cz_odds_wt_xyftoa_bll == null)
            {
                cz_odds_wt_xyftoa_bll = new cz_odds_wt_xyftoaBLL();
            }
            if (cz_odds_wt_xyftsg_bll == null)
            {
                cz_odds_wt_xyftsg_bll = new cz_odds_wt_xyftsgBLL();
            }
            if (cz_odds_wt_happycar_bll == null)
            {
                cz_odds_wt_happycar_bll = new cz_odds_wt_happycarBLL();
            }
            if (cz_jp_odds_bll == null)
            {
                cz_jp_odds_bll = new cz_jp_oddsBLL();
            }
            if (cz_system_log_bll == null)
            {
                cz_system_log_bll = new cz_system_logBLL();
            }
            if (cz_admin_sysconfig_bll == null)
            {
                cz_admin_sysconfig_bll = new cz_admin_sysconfigBLL();
            }
            if (cz_lm_group_set_bll == null)
            {
                cz_lm_group_set_bll = new cz_lm_group_setBLL();
            }
            if ((redisHelper == null) && FileCacheHelper.get_RedisStatOnline().Equals(1))
            {
                redisHelper = new RedisHelper(FileCacheHelper.get_GetRedisDBIndex());
            }
            if (cz_lm_number_amount_six_bll == null)
            {
                cz_lm_number_amount_six_bll = new cz_lm_number_amount_sixBLL();
            }
        }
    }
}

