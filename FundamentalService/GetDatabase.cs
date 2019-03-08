using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundamentalService
{
    class GetDatabase
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Config                                                          |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private static string accessToken = "qewrasd5a1e8q4120as5d4qw0e21asd56qw0e5as3q68q6";
        private static string DatabaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string Username = ConfigurationManager.AppSettings["DatabaseUsername"];
        private static string Password = ConfigurationManager.AppSettings["DatabasePassword"];
        private static string AuthUsername = ConfigurationManager.AppSettings["ServerUsername"];
        private static string AuthPassword = ConfigurationManager.AppSettings["ServerPassword"];
        private static Plog log = new Plog();
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Model                                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public class FinanceInfo
        {

            public FinanceInfo() { }

            // Properties.
            public string Date { get; set; }
            public string Symbol { get; set; }
            public string Year { get; set; }
            public string Quarter { get; set; }
            public string Asset { get; set; }
            public string Liabilities { get; set; }
            public string Equity { get; set; }
            public string Paid_up_cap { get; set; }
            public string Revenue { get; set; }
            public string NetProfit { get; set; }
            public string EPS { get; set; }
            public string ROA { get; set; }
            public string ROE { get; set; }
            public string NetProfitMargin { get; set; }
        }
        public class FinanceStat
        {

            public FinanceStat() { }

            // Properties.
            public string Date { get; set; }
            public string Symbol { get; set; }
            public string Year { get; set; }
            public string Lastprice { get; set; }
            public string Market_cap { get; set; }
            public string FS_date { get; set; }
            public string PE { get; set; }
            public string PBV { get; set; }
            public string BookValue_Share { get; set; }
            public string Dvd_Yield { get; set; }
        }
        public class Fundamental
        {

            public Fundamental() { }

            // Properties.
            public List<FinanceInfo> finance_info_yearly { get; set; }
            public List<FinanceInfo> finance_info_quarter { get; set; }
            public List<FinanceStat> finance_stat_yearly { get; set; }
            public List<FinanceStat> finance_stat_daily { get; set; }
        }
        public class Assets
        {

            public Assets() { }

            // Properties.
            public string Year { get; set; }
            public string Symbol { get; set; }
            public string Cash_equivalents { get; set; }
            public string Short_Investments { get; set; }
            public string Other_receivable { get; set; }
            public string Short_loans { get; set; }
            public string Related_parties { get; set; }
            public string Inventories { get; set; }
            public string Finished_goods { get; set; }
            public string Raw_material { get; set; }
            public string Derivative_assets_cerrent { get; set; }
            public string Other_short_receivable { get; set; }
            public string Other_current_assets { get; set; }
            public string Other_current_assets_others { get; set; }
            public string Total_current_assets { get; set; }
            public string Investment_account { get; set; }
            public string Associates { get; set; }
            public string For_sale_investments { get; set; }
            public string Long_investments { get; set; }
            public string Investment_properties_net { get; set; }
            public string Long_loans { get; set; }
            public string Assets_agreements { get; set; }
            public string Goodwill_net { get; set; }
            public string Goodwill { get; set; }
            public string Investment_net { get; set; }
            public string To_maturity_investments { get; set; }
            public string Investment_account_net { get; set; }
            public string Associates_net { get; set; }
            public string Affiliates { get; set; }
            public string Allowance_impairment_investments { get; set; }
            public string Loans_receivable_net { get; set; }
            public string Loans_and_receivables { get; set; }
            public string Accrued_interest_receivables { get; set; }
            public string Less_doubtful_accounts { get; set; }
            public string Property_net { get; set; }
            public string Property { get; set; }
            public string Improvement_property { get; set; }
            public string Less_depreciation_property { get; set; }
            public string Intangible_assets { get; set; }
            public string Other_intangible_assets { get; set; }
            public string Less_accumulated { get; set; }
            public string Non_derivative_assets { get; set; }
            public string Financial_assets { get; set; }
            public string Other_financial { get; set; }
            public string Defferred_tax_assets { get; set; }
            public string Non_current_assets { get; set; }
            public string Non_current_assets_other { get; set; }
            public string Other_receivables_net { get; set; }
            public string Other_assets_net { get; set; }
            public string Advence_payments { get; set; }
            public string Prepayments { get; set; }
            public string Refundable_deposits { get; set; }
            public string Other_assets_other { get; set; }
            public string Total_non_current_assets { get; set; }
            public string Total_assets { get; set; }
            public string Related_parties_short { get; set; }
            public string Prepayments_current { get; set; }
            public string Short_Investments_restricted { get; set; }
            public string Other_current_financial { get; set; }
            public string Investment_joint_ventures { get; set; }
            public string Concession_rights { get; set; }
            public string Other_parties { get; set; }
            public string Related_parties_receivable { get; set; }
            public string Less_allowance_doubtful { get; set; }
            public string Unbilled_receivables { get; set; }
            public string Other_parties_unbilled { get; set; }
            public string Current_portion_long { get; set; }
            public string Related_parties_long { get; set; }
            public string Real_estate_costs_Inventories { get; set; }
            public string Real_estate_costs { get; set; }
            public string Withholding_tax { get; set; }
            public string Avance_payments { get; set; }
            public string Refundable_deposits_non { get; set; }
            public string Avance_payments_current { get; set; }
            public string Cash_restricted { get; set; }
            public string Investment_properties { get; set; }
            public string Less_accumulated_depreciation { get; set; }
            public string Less_allowance_impairment { get; set; }
            public string Related_parties_long_loans { get; set; }
            public string Leasehold_right { get; set; }
            public string Less_impairment { get; set; }
            public string Debt_securities { get; set; }
            public string Refundable_deposits_current { get; set; }
            public string Other_parties_short { get; set; }
            public string Dividend_receivables { get; set; }
            public string Deferred_expenditure { get; set; }
            public string Loans_financial_institutions { get; set; }
            public string Deferred_revenue { get; set; }
            public string Less_revaluation_allowance { get; set; }
            public string Customers_liabilities { get; set; }
            public string Assets_forclosed { get; set; }
            public string Derivative_assets { get; set; }
            public string Other_parties_short_receivable { get; set; }
            public string Accrued_income { get; set; }
            public string Net_current_portion { get; set; }
            public string Current_portion_long_receivables { get; set; }
            public string Related_parties_long_receivable { get; set; }
            public string Accrued_interest_current { get; set; }
            public string Assets_under_concession { get; set; }
            public string Software_licances { get; set; }
            public string Other_parties_long_current { get; set; }
            public string Other_parties_long { get; set; }
            public string Goods_in_transit { get; set; }
            public string Work_in_progress { get; set; }
            public string Less_allowance_diminution { get; set; }
            public string Amount_retention_construction { get; set; }
            public string Property_finance_leases { get; set; }
            public string Less_allowance_property { get; set; }
            public string Trade_mark { get; set; }
            public string Other_parties_portion { get; set; }
            public string Prepaid_expenses { get; set; }
            public string Refundable_income { get; set; }
            public string Refundable_value_added_tax { get; set; }
            public string Affiliates_investment { get; set; }
            public string Biological_assets_net { get; set; }
            public string Biological_assets { get; set; }
            public string Receivables_value_added_tax { get; set; }
            public string Other_non_current_financial { get; set; }
            public string Other_parties_long_receivables { get; set; }
            public string Non_current_for_sale { get; set; }
            public string Less_allowance_investments { get; set; }
            public string Interbank_money_market { get; set; }
            public string Receivables_clearing_house { get; set; }
            public string Claim_security { get; set; }
            public string Account_receivables { get; set; }
            public string Other_investment { get; set; }
            public string Investment_trading_securities { get; set; }
            public string Domestic_non_interest { get; set; }
            public string Available_sale_investments { get; set; }
            public string Estate_for_sales { get; set; }
            public string Value_added_tax { get; set; }
            public string Domestic_interrest { get; set; }
            public string Foreign_interest_bearing { get; set; }
            public string Foreign_non_interest_bearing { get; set; }
            public string Investment_trading_securities_net { get; set; }
            public string Investment_joint_ventures_net { get; set; }
            public string Related_parties_portion { get; set; }
            public string Securities_derivatives_receivables { get; set; }
            public string Revaluation_property { get; set; }
            public string Receivables_selling_investments { get; set; }
            public string Deferred_expenditure_assets { get; set; }
            public string Update_Date { get; set; }
        }
        public class Debt
        {

            public Debt() { }

            // Properties.
            public string Year { get; set; }
            public string Symbol { get; set; }
            public string Short_borrowing { get; set; }
            public string Trade_other_payable { get; set; }
            public string Short_account_payables { get; set; }
            public string Long_liabilities { get; set; }
            public string Derivative_liabilities { get; set; }
            public string Short_provisions { get; set; }
            public string Current_liabilities { get; set; }
            public string Tax_payables { get; set; }
            public string Current_liabilities_others { get; set; }
            public string Total_current_liabilities { get; set; }
            public string Net_long_liabilities { get; set; }
            public string Non_derivative_liabilities { get; set; }
            public string Long_provisions { get; set; }
            public string Post_employee_obligtions { get; set; }
            public string Defferred_tax_liabilities { get; set; }
            public string Non_current_liabilities { get; set; }
            public string Refundable_deposits { get; set; }
            public string Non_current_liabilities_other { get; set; }
            public string Total_non_current_liabilities { get; set; }
            public string Total_liabilities { get; set; }
            public string Current_portion { get; set; }
            public string Debt_certificates { get; set; }
            public string Other_partties_short { get; set; }
            public string Related_parties_short { get; set; }
            public string Long_borrowing { get; set; }
            public string Finance_lease_liabilities { get; set; }
            public string Accrued_expense { get; set; }
            public string Finance_lease_liabilities_net { get; set; }
            public string Other_payable { get; set; }
            public string Income_tax_payable { get; set; }
            public string Other_liabilities { get; set; }
            public string Liabilities_concession_long { get; set; }
            public string Liabilities_under_concession { get; set; }
            public string Current_portion_received { get; set; }
            public string Accrued_concession_fees { get; set; }
            public string Liabilities_concession_long_net { get; set; }
            public string Liabilities_under_concession_net { get; set; }
            public string Borrowings_deposits { get; set; }
            public string Borrowings { get; set; }
            public string Short_borrowings { get; set; }
            public string Long_borrowings { get; set; }
            public string Accrues_interest_payable { get; set; }
            public string Debentures_other_debt { get; set; }
            public string Financial_liabilities { get; set; }
            public string Other_financial_liabilities { get; set; }
            public string Provisions { get; set; }
            public string Account_payables { get; set; }
            public string Other_partties_trade { get; set; }
            public string Related_parties_trade { get; set; }
            public string Advances_short_loans { get; set; }
            public string Related_parties_short_loans { get; set; }
            public string Debt_certificates_long { get; set; }
            public string Amount_retention { get; set; }
            public string Accrued_interest_payable { get; set; }
            public string Long_borrowings_financial { get; set; }
            public string Net_deferred_income { get; set; }
            public string Net_received_customers { get; set; }
            public string Other_partties_short_loans { get; set; }
            public string Long_borrowings_related { get; set; }
            public string Net_current_portion { get; set; }
            public string Other_partties_portion { get; set; }
            public string Current_portion_received_net { get; set; }
            public string Long_borrowings_parties { get; set; }
            public string Retention { get; set; }
            public string Long_borrowings_other_parties { get; set; }
            public string Interbank_money_market { get; set; }
            public string Deposits { get; set; }
            public string Liabilities_payable_demand { get; set; }
            public string Liabilities_under_acceptances { get; set; }
            public string Derivative_liabilities_financial { get; set; }
            public string Land_cost_payable { get; set; }
            public string Current_benefit_obligations { get; set; }
            public string Excise_duty_payable { get; set; }
            public string Added_tax_payable { get; set; }
            public string Refundable_deposits_liabilities { get; set; }
            public string Dividend_payable { get; set; }
            public string Deposits_bath { get; set; }
            public string Deposits_foreign_currencies { get; set; }
            public string Liabilities_non_current { get; set; }
            public string Payable_clearing_house { get; set; }
            public string Securities_business_payables { get; set; }
            public string Claims_on_security { get; set; }
            public string Liabilities_turst_receipts { get; set; }
            public string Payables_purchases_securities { get; set; }
            public string Excess_loss_cost { get; set; }
            public string Current_portion_account { get; set; }
            public string Other_parties_account { get; set; }
            public string Domestic_interest_bearing { get; set; }
            public string Domestic_non_interest_bearing { get; set; }
            public string Foreign_interest_bearing { get; set; }
            public string Foreign_non_interest_bearing { get; set; }
            public string Financial_fair_value { get; set; }
            public string Bank_overdrafts { get; set; }
            public string Update_Date { get; set; }
        }
        public class ShareHolderEquity
        {
            public ShareHolderEquity() { }
            // Properties.
            public string Year { get; set; }
            public string Symbol { get; set; }
            public string Share_capital { get; set; }
            public string Share_ordinary_shares { get; set; }
            public string Fully_shares_capital { get; set; }
            public string Fully_ordinary_shares { get; set; }
            public string Premium_shares_capital { get; set; }
            public string Premium_ordinary_shares { get; set; }
            public string Retained_earnings { get; set; }
            public string Retained_earnings_appropriated { get; set; }
            public string Legal_statutory_reserves { get; set; }
            public string Self_insurance_reserve { get; set; }
            public string Retained_earnings_unappropriated { get; set; }
            public string Other_components_equity { get; set; }
            public string Other_surplus { get; set; }
            public string Revaluation_surplus { get; set; }
            public string Unrealised_gain_investments { get; set; }
            public string Gains_instruments { get; set; }
            public string Surplus_subsidiaries_associates { get; set; }
            public string Currency_translation_changes { get; set; }
            public string Equity_attributable { get; set; }
            public string Non_current_interests { get; set; }
            public string Total_equity { get; set; }
            public string Expansion_reserve { get; set; }
            public string Other_reserves { get; set; }
            public string Expired_warrants_surplus { get; set; }
            public string Surplus_combinations_control { get; set; }
            public string Other_surplus_others { get; set; }
            public string Share_subscription_received { get; set; }
            public string Other_items { get; set; }
            public string Warrants_options { get; set; }
            public string Preferred_shares { get; set; }
            public string Preferred_shares_fully { get; set; }
            public string Treasury_shares_reserve { get; set; }
            public string Other_sub_associates { get; set; }
            public string Premium_capital_reduction { get; set; }
            public string Surplus_subsidiaries_associates_increase { get; set; }
            public string Treasury_shares_subsidiaries { get; set; }
            public string Number_treasury_shares { get; set; }
            public string Treasury_shares { get; set; }
            public string Shares_subsidiaries { get; set; }
            public string Loan_repayment_reserve { get; set; }
            public string Premium_treasury_shares { get; set; }
            public string Treasury_ordinary_shares { get; set; }
            public string Preferred_shares_premium { get; set; }
            public string Revalution_fixed_assets { get; set; }
            public string Revaluation_surplus_sub { get; set; }
            public string Revalution_fixed_assets_sub { get; set; }
            public string Update_Date { get; set; }

        }
        public class Income
        {
            public Income() { }
            // Properties.
            public string Year { get; set; }
            public string Symbol { get; set; }
            public string Revenues_rendering_services { get; set; }
            public string Other_income { get; set; }
            public string Gain_foreign_exchange { get; set; }
            public string Other_income_others { get; set; }
            public string Shares_investments_accounted { get; set; }
            public string Total_revenues { get; set; }
            public string Cost_sale_rendering_services { get; set; }
            public string Selling_admin_expenses { get; set; }
            public string Selling_expenses { get; set; }
            public string Admin_expenses { get; set; }
            public string Other_expenses { get; set; }
            public string Other_expenses_others { get; set; }
            public string Total_expenses { get; set; }
            public string Profit_income_tax { get; set; }
            public string Finance_costs { get; set; }
            public string Income_tax_expenses { get; set; }
            public string Net_profit { get; set; }
            public string Profit_equity_holders { get; set; }
            public string Profit_non_controlling { get; set; }
            public string Basic_earnings_share { get; set; }
            public string Interest { get; set; }
            public string Loans { get; set; }
            public string Interbank_money_market { get; set; }
            public string Finance_lease_income { get; set; }
            public string Investment_trading_transactions { get; set; }
            public string Interest_expense { get; set; }
            public string Deposits { get; set; }
            public string Interbank_money_market_interest { get; set; }
            public string Deposits_protection_agency { get; set; }
            public string Fee_changes { get; set; }
            public string Interest_discounts { get; set; }
            public string Net_interest_income { get; set; }
            public string Fees_service_income { get; set; }
            public string Acceptances_guarantees { get; set; }
            public string Others_fee_service { get; set; }
            public string Fees_service_expenses { get; set; }
            public string Net_fees_service_income { get; set; }
            public string Gain_Trading_foreign { get; set; }
            public string Gain_on_foreign_exchange { get; set; }
            public string Gains_derivative_trading { get; set; }
            public string Other_gain_trading { get; set; }
            public string Gain_selling_investments { get; set; }
            public string Gain_on_investments { get; set; }
            public string Gain_disposal_investment { get; set; }
            public string Reversal_impariment_investment { get; set; }
            public string Share_profit_loss_investment { get; set; }
            public string Share_profit_investment_account { get; set; }
            public string Other_operating_incomes { get; set; }
            public string Gain_disposal_properties { get; set; }
            public string Other_income_operation { get; set; }
            public string Other_operating_expenses { get; set; }
            public string Personnel_expenses { get; set; }
            public string Premises_equiment_expenses { get; set; }
            public string Taxes_duties { get; set; }
            public string Management_remuneration { get; set; }
            public string Directors_remuneration { get; set; }
            public string Other_expenses_operation { get; set; }
            public string Bad_debt_securities { get; set; }
            public string Bad_debt_tax_expenses { get; set; }
            public string Before_income_tax_expenses { get; set; }
            public string Diluted_earning_share { get; set; }
            public string Revenues_sales_real { get; set; }
            public string Revenues_construction_contracts { get; set; }
            public string Revenues_rendering_service { get; set; }
            public string Interest_income { get; set; }
            public string Gain_disposal_investments { get; set; }
            public string Cost_sales_property { get; set; }
            public string Cost_construction_services { get; set; }
            public string Cost_rendering_services { get; set; }
            public string Revenues_from_sales { get; set; }
            public string Cost_goods_sold { get; set; }
            public string Management_directors_remuneration { get; set; }
            public string Management_remuneration_directors { get; set; }
            public string Shares_loss_using_equity { get; set; }
            public string Loss_disposal_fixed { get; set; }
            public string Bad_debt_doubtful { get; set; }
            public string Impairment_fixed_assets { get; set; }
            public string Impairment_intangible_assets { get; set; }
            public string Loss_diminution_investories { get; set; }
            public string Short_borrowings { get; set; }
            public string Long_borrowings { get; set; }
            public string Brokerage_fee { get; set; }
            public string Management_remuneration_operating { get; set; }
            public string Loss_disposal_fixed_operating { get; set; }
            public string Bad_debt_reversal { get; set; }
            public string Gain_properties_foreclosed { get; set; }
            public string Gain_disposal_other_assets { get; set; }
            public string Concession_fee_excise_tax { get; set; }
            public string Loss_currency_exchange { get; set; }
            public string Loss_fair_investment { get; set; }
            public string Impairment_loss_other_assets { get; set; }
            public string Depreciation_amortisation { get; set; }
            public string Dividend_income { get; set; }
            public string Rental_services_income { get; set; }
            public string Gain_disposal_investment_income { get; set; }
            public string Gain_fair_investments { get; set; }
            public string Gain_fair_properties { get; set; }
            public string Reversal_impariment_assets { get; set; }
            public string Directors_remuneration_manager { get; set; }
            public string Loss_derivative_trading { get; set; }
            public string Exploration_expenses { get; set; }
            public string Writeoff_development_costs { get; set; }
            public string Gain_disposal_assets { get; set; }
            public string Reversal_loss_fixed_assets { get; set; }
            public string Bad_debt_recovery { get; set; }
            public string Gain_derivative_trading { get; set; }
            public string Royalty_fee { get; set; }
            public string Loss_disposal_investments { get; set; }
            public string Specific_business_tax { get; set; }
            public string Other_interest { get; set; }
            public string Reversal_impairment_forecloses { get; set; }
            public string Loss_disposal_foreclosed { get; set; }
            public string Impairment_loss_foreclosed { get; set; }
            public string Provision_expenses { get; set; }
            public string Financial_instrument_designated { get; set; }
            public string Other_item { get; set; }
            public string profit_discontinued_operations { get; set; }
            public string Update_Date { get; set; }

        }
        public class ComprehensiveIncome
        {
            public ComprehensiveIncome() { }
            // Properties.
            public string Year { get; set; }
            public string Symbol { get; set; }
            public string Net_profit { get; set; }
            public string Unrealised_gain { get; set; }
            public string Actuarial_gain { get; set; }
            public string Gain_cash_flow { get; set; }
            public string Exchange_differces { get; set; }
            public string Share_other_comprehensive { get; set; }
            public string Income_tax_relating { get; set; }
            public string Total_comprehensive_income { get; set; }
            public string Total_holders_parent { get; set; }
            public string Total_non_controlling { get; set; }
            public string Change_assets_revaluation { get; set; }
            public string Other_comprehensive_income { get; set; }
            public string Hedges_foreign_operations { get; set; }
            public string Update_Date { get; set; }

        }
        public class CashFlow
        {
            public CashFlow() { }
            // Properties.
            public string Year { get; set; }
            public string Symbol { get; set; }
            public string Period_net_profit { get; set; }
            public string Depreciation_amortisation { get; set; }
            public string Bad_debt_doubleful { get; set; }
            public string Obsolescence { get; set; }
            public string Diminotion_value_inventories { get; set; }
            public string Attributable_non_controlling { get; set; }
            public string Share_investments_accounted { get; set; }
            public string Unrealised_foreign_exchange { get; set; }
            public string Impairment_other_assets { get; set; }
            public string Sales_investments_subsidiaries { get; set; }
            public string Disposal_fixed_assets { get; set; }
            public string Fair_value_adjustments { get; set; }
            public string Finance_costs { get; set; }
            public string Income_tax_expenses { get; set; }
            public string Other_reconciliation_items { get; set; }
            public string Cash_flow_operations { get; set; }
            public string Operating_assets { get; set; }
            public string Trade_account { get; set; }
            public string Other_receivables { get; set; }
            public string Inventories { get; set; }
            public string Other_current_assets { get; set; }
            public string Other_non_current_assets { get; set; }
            public string Operating_liabilities { get; set; }
            public string Trade_account_payables { get; set; }
            public string Other_payables { get; set; }
            public string Other_current_liabilities { get; set; }
            public string Other_non_current_liabilities { get; set; }
            public string Cash_generated_operations { get; set; }
            public string Income_tax_paid { get; set; }
            public string Net_cash_provided { get; set; }
            public string Short_term_investments { get; set; }
            public string Long_term_investments { get; set; }
            public string Increase_long_investments { get; set; }
            public string Decrease_long_investments { get; set; }
            public string Investment_subsidiaries { get; set; }
            public string Increase_investment_subsidiaries { get; set; }
            public string Decrease_investment_subsidiaries { get; set; }
            public string Advances_short_loans { get; set; }
            public string Long_term_loans { get; set; }
            public string Increase_long_loans { get; set; }
            public string Decrease_long_loans { get; set; }
            public string Property_plant_equipments { get; set; }
            public string Proceeds_disposal { get; set; }
            public string Purchases_property { get; set; }
            public string Intangible_assets { get; set; }
            public string Increase_intangible_assets { get; set; }
            public string Assets_under_concession { get; set; }
            public string Increase_assets_under_concession { get; set; }
            public string Dividends_received { get; set; }
            public string Interest_received { get; set; }
            public string Other_item_received { get; set; }
            public string Net_provided_investing { get; set; }
            public string Short_borrowing_financial { get; set; }
            public string Long_borrowing_financial { get; set; }
            public string Increase_long_borrowings { get; set; }
            public string Decrease_long_borrowings { get; set; }
            public string Finance_lease_contract { get; set; }
            public string Decrease_finance_lease { get; set; }
            public string Proceeds_issuance { get; set; }
            public string Dividend_paid { get; set; }
            public string Interest_paid { get; set; }
            public string Other_item_paid { get; set; }
            public string Net_provided_financing { get; set; }
            public string Net_increase_cash { get; set; }
            public string Effect_exchange_rate { get; set; }
            public string Differences_financial_translation { get; set; }
            public string Cash_beginning_balance { get; set; }
            public string Cash_ending_balance { get; set; }
            public string Other_receivables_related { get; set; }
            public string Other_payables_related { get; set; }
            public string Before_financial_costs { get; set; }
            public string Long_borrowing_parties { get; set; }
            public string Decrease_long_borrowings_parties { get; set; }
            public string Debt_instruments { get; set; }
            public string Proceeds_issuance_debt { get; set; }
            public string Depreciation { get; set; }
            public string Amortisation { get; set; }
            public string Increase_long_borrowings_parties { get; set; }
            public string Impairment_loss_fixed_assets { get; set; }
            public string Disposal_other_assets { get; set; }
            public string Other_investment { get; set; }
            public string Increase_other_investment { get; set; }
            public string Decrease_loans { get; set; }
            public string Repayment_debentures { get; set; }
            public string Loss_write_fixed { get; set; }
            public string Dividend_interest_income { get; set; }
            public string Interest_received_income { get; set; }
            public string Interest_received_s { get; set; }
            public string Interest_paid_s { get; set; }
            public string Other_item_dividend { get; set; }
            public string Disposal_properties_foreclosed { get; set; }
            public string Investment_properties { get; set; }
            public string Increase_investment_properties { get; set; }
            public string Decrease_investment_properties { get; set; }
            public string Short_borrowing_related { get; set; }
            public string Disposal_other_investments { get; set; }
            public string Fair_adjustments_investments { get; set; }
            public string Advances_short_loans_related { get; set; }
            public string Long_loans_related { get; set; }
            public string Increase_long_loans_related { get; set; }
            public string Decrease_loan_loans_related { get; set; }
            public string Restricted_deposits_financial { get; set; }
            public string Decrease_debt_instruments { get; set; }
            public string Other_item { get; set; }
            public string Advances_short_loans_partie { get; set; }
            public string Interbank_money_market { get; set; }
            public string Short_investments { get; set; }
            public string Properties_foreclosed_assets { get; set; }
            public string Deposits { get; set; }
            public string Interbank_money_operating { get; set; }
            public string Liabilities_payable_demand { get; set; }
            public string Borrowings_operating { get; set; }
            public string Dividend_received_income { get; set; }
            public string Writeoff_other_assets { get; set; }
            public string Fair_value_adjustments_investments { get; set; }
            public string Reclassification_investments { get; set; }
            public string Impairment_loss_investment { get; set; }
            public string Impairment_loss_properties { get; set; }
            public string Debt_restructuring { get; set; }
            public string Trade_account_receivables { get; set; }
            public string Trade_account_payables_related { get; set; }
            public string Decrease_other_investment { get; set; }
            public string Other_loans_related { get; set; }
            public string Increase_other_loans { get; set; }
            public string Writeoff_intangible_assets { get; set; }
            public string Purchase_treasury_shares { get; set; }
            public string Decrease_intangible_assets { get; set; }
            public string Decrease_other_loans { get; set; }
            public string Long_borrowing_related { get; set; }
            public string Decrease_long_borrowings_related { get; set; }
            public string Proceeds_advance { get; set; }
            public string Other_loan_other_parties { get; set; }
            public string Decrease_other_loans_parties { get; set; }
            public string Loss_writoff_assets { get; set; }
            public string Impairment_loss_identifiable { get; set; }
            public string Properties_foreclosed { get; set; }
            public string Disposal_intangible_assets { get; set; }
            public string Other_loan_financial { get; set; }
            public string Receivables_clearing_house { get; set; }
            public string Securities_derivatives_business_operating { get; set; }
            public string Income_tax_payable { get; set; }
            public string Provisions { get; set; }
            public string Accrued_expenses { get; set; }
            public string Payables_clearing_house { get; set; }
            public string Securities_derivatives_business { get; set; }
            public string Income_tax_payable_operating { get; set; }
            public string Increase_loan_other_parties { get; set; }
            public string Increase_loan_financial { get; set; }
            public string Decrease_loan_financial { get; set; }
            public string Short_borrowing_other_parties { get; set; }
            public string Other_loans_related_parties { get; set; }
            public string Increase_loans_related { get; set; }
            public string Dividend_received { get; set; }
            public string Disposal_derivative_trading { get; set; }
            public string Increase_long_borrowings_related { get; set; }
            public string Accrued_income { get; set; }
            public string Other_derivatives_assets { get; set; }
            public string Derivatives_liabilities { get; set; }
            public string Increase_finance_lease { get; set; }
            public string Loss_write_investments { get; set; }
            public string Loss_write_properties_foreclosed { get; set; }
            public string Dividend_paid_s { get; set; }
            public string Liabilities_under_trust { get; set; }
            public string Update_Date { get; set; }
        }
        public class Statement
        {

            public Statement() { }

            // Properties.
            public List<Assets> asset { get; set; }
            public List<Debt> debt { get; set; }
            public List<ShareHolderEquity> share_holder_equity { get; set; }
            public List<Income> income { get; set; }
            public List<ComprehensiveIncome> comprehensive_incomes { get; set; }
            public List<CashFlow> cash_flow { get; set; }
        }
        public class KaohoonData
        {
            public KaohoonData() { }

            // Properties.
            public string Kaohoon_data_id { get; set; } = "null";
            public string Symbol { get; set; } = "null";
            public string Date { get; set; } = "null";
            public string Year { get; set; } = "null";
            public string Quarter { get; set; } = "null";
            public string TargetPrice { get; set; } = "null";
            public string Trends { get; set; } = "null";
            public string Divide { get; set; } = "null";
            public string Topic { get; set; } = "null";

        }
        public class Message
        {

            public Message()
            {
                message = "error !!";
            }

            // Properties.
            public string message { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public dynamic GetFundamentalDB(string input)
        {
            var param = input.Split('/');
            switch (input)
            {
                case "fundamental":
                    return GetFundamental();
                case "fundamental/finance_info_yearly":
                    return GetFinanceInfoYearly();
                case "fundamental/finance_info_quarter":
                    return GetFinanceInfoQuarter();
                case "fundamental/finance_stat_yearly":
                    return GetFinanceStatYearly();
                case "fundamental/finance_stat_daily":
                    return GetFinanceStatDaily();
                default:
                    {
                        if (param[1] == "finance_info_yearly" && param.Length == 3)
                            return GetFinanceInfoYearly(param[2]);
                        else if (param[1] == "finance_info_yearly" && param[2] == "year" && param.Length == 4)
                            return GetFinanceInfoYearly(null, param[3]);
                        else if (param[1] == "finance_info_yearly" && param[2] == "date" && param.Length == 4)
                            return GetFinanceInfoYearly(null, null, param[3]);
                        else if (param[1] == "finance_info_yearly" && param[2] == "date" && param.Length == 5)
                            return GetFinanceInfoYearly(param[3], null, param[4]);
                        else if (param[1] == "finance_info_yearly" && param.Length == 4)
                            return GetFinanceInfoYearly(param[2], param[3]);
                        else if (param[1] == "finance_info_quarter" && param.Length == 3)
                            return GetFinanceInfoQuarter(param[2]);
                        else if (param[1] == "finance_info_quarter" && param[2] == "year" && param.Length == 4)
                            return GetFinanceInfoQuarter(null, param[3]);
                        else if (param[1] == "finance_info_quarter" && param[2] == "date" && param.Length == 4)
                            return GetFinanceInfoQuarter(null, null, param[3]);
                        else if (param[1] == "finance_info_quarter" && param[2] == "date" && param.Length == 5)
                            return GetFinanceInfoQuarter(param[3], null, param[4]);
                        else if (param[1] == "finance_info_quarter" && param.Length == 4)
                            return GetFinanceInfoQuarter(param[2], param[3]);
                        else if (param[1] == "finance_stat_yearly" && param.Length == 3)
                            return GetFinanceStatYearly(param[2]);
                        else if (param[1] == "finance_stat_yearly" && param[2] == "year" && param.Length == 4)
                            return GetFinanceStatYearly(null, param[3]);
                        else if (param[1] == "finance_stat_yearly" && param[2] == "date" && param.Length == 4)
                            return GetFinanceStatYearly(null, null, param[3]);
                        else if (param[1] == "finance_stat_yearly" && param[2] == "date" && param.Length == 5)
                            return GetFinanceStatYearly(param[3], null, param[4]);
                        else if (param[1] == "finance_stat_yearly" && param.Length == 4)
                            return GetFinanceStatYearly(param[2], param[3]);
                        else if (param[1] == "finance_stat_daily" && param.Length == 3)
                            return GetFinanceStatDaily(param[2]);
                        else if (param[1] == "finance_stat_daily" && param[2] == "year" && param.Length == 4)
                            return GetFinanceStatDaily(null, param[3]);
                        else if (param[1] == "finance_stat_daily" && param[2] == "date" && param.Length == 4)
                            return GetFinanceStatDaily(null, null, param[3]);
                        else if (param[1] == "finance_stat_daily" && param[2] == "date" && param.Length == 5)
                            return GetFinanceStatDaily(param[3], null, param[4]);
                        else if (param[1] == "finance_stat_daily" && param.Length == 4)
                            return GetFinanceStatDaily(param[2], param[3]);
                        else if (param[0] == "fundamental" && param[1] == "year" && param.Length == 3)
                            return GetFundamental(null, param[2]);
                        else if (param[0] == "fundamental" && param[1] == "date" && param.Length == 3)
                            return GetFundamental(null, null, param[2]);
                        else if (param[0] == "fundamental" && param[1] == "date" && param.Length == 4)
                            return GetFundamental(param[2], null, param[3]);
                        else if (param[0] == "fundamental" && param.Length == 2)
                            return GetFundamental(param[1]);
                        else if (param[0] == "fundamental" && param.Length == 3)
                            return GetFundamental(param[1], param[2]);
                        else
                            return new Message();
                    }
            }
        }
        public dynamic GetStateMentDB(string input)
        {
            var param = input.Split('/');
            switch (input)
            {
                case "statement":
                    return GetStatement();
                case "statement/asset":
                    return GetAssets();
                case "statement/debt":
                    return GetDebt();
                case "statement/share_holder_equity":
                    return GetShareHolderEquity();
                case "statement/income":
                    return GetIncome();
                case "statement/comprehensive_income":
                    return GetComprehensiveIncome();
                case "statement/cash_flow":
                    return GetCashFlow();
                default:
                    {
                        if (param[1] == "asset")
                        {
                            if (param[2] == "lastupdate" && param.Length == 3)
                                return GetAssets(null, true);
                            else if (param[2] == "symbol" && param.Length == 4)
                                return GetAssets(param[3]);
                            else if (param[2] == "lastupdate&symbol" && param.Length == 4)
                                return GetAssets(param[3], true);
                            else
                                return new Message();
                        }
                        else if (param[1] == "debt")
                        {
                            if (param[2] == "lastupdate" && param.Length == 3)
                                return GetDebt(null, true);
                            else if (param[2] == "symbol" && param.Length == 4)
                                return GetDebt(param[3]);
                            else if (param[2] == "lastupdate&symbol" && param.Length == 4)
                                return GetDebt(param[3], true);
                            else
                                return new Message();
                        }
                        else if (param[1] == "share_holder_equity")
                        {
                            if (param[2] == "lastupdate" && param.Length == 3)
                                return GetShareHolderEquity(null, true);
                            else if (param[2] == "symbol" && param.Length == 4)
                                return GetShareHolderEquity(param[3]);
                            else if (param[2] == "lastupdate&symbol" && param.Length == 4)
                                return GetShareHolderEquity(param[3], true);
                            else
                                return new Message();
                        }
                        else if (param[1] == "income")
                        {
                            if (param[2] == "lastupdate" && param.Length == 3)
                                return GetIncome(null, true);
                            else if (param[2] == "symbol" && param.Length == 4)
                                return GetIncome(param[3]);
                            else if (param[2] == "lastupdate&symbol" && param.Length == 4)
                                return GetIncome(param[3], true);
                            else
                                return new Message();
                        }
                        else if (param[1] == "comprehensive_income")
                        {
                            if (param[2] == "lastupdate" && param.Length == 3)
                                return GetComprehensiveIncome(null, true);
                            else if (param[2] == "symbol" && param.Length == 4)
                                return GetComprehensiveIncome(param[3]);
                            else if (param[2] == "lastupdate&symbol" && param.Length == 4)
                                return GetComprehensiveIncome(param[3], true);
                            else
                                return new Message();
                        }
                        else if (param[1] == "cash_flow")
                        {
                            if (param[2] == "lastupdate" && param.Length == 3)
                                return GetCashFlow(null, true);
                            else if (param[2] == "symbol" && param.Length == 4)
                                return GetCashFlow(param[3]);
                            else if (param[2] == "lastupdate&symbol" && param.Length == 4)
                                return GetCashFlow(param[3], true);
                            else
                                return new Message();
                        }
                        else if (param[1] == "lastupdate" && param.Length == 2)
                            return GetStatement(null, true);
                        else if (param[1] == "symbol" && param.Length == 3)
                            return GetStatement(param[2]);
                        else if (param[1] == "lastupdate&symbol" && param.Length == 3)
                            return GetStatement(param[2], true);
                        else
                            return new Message();
                    }
            }
        }
        public dynamic GetKaohoonDB(string input)
        {
            var param = input.Split('/');
            switch (input)
            {
                case "kaohoon":
                    return GetKaohoonData();
                default:
                    {
                        if (param[0] == "kaohoon" && param[1] == "date" && param.Length == 3)
                            return GetKaohoonData(null, param[2]);
                        else if (param[0] == "kaohoon" && param.Length == 2)
                            return GetKaohoonData(param[1]);
                        else if (param[0] == "kaohoon" && param.Length == 3)
                            return GetKaohoonData(param[1], param[2]);
                        else
                            return new Message();
                    }
            }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Access Token Function                                           |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public string AccessToken(string input)
        {
            dynamic json = JsonConvert.DeserializeObject(input);
            if (json.AccessToken == accessToken)
                return "true";
            else
                return "false";
        }
        public string Authorization(string input)
        {
            dynamic json = JsonConvert.DeserializeObject(input);
            var security = new EncryptionHelper();
            if (json.Username == security.Encrypt(AuthUsername) && json.Password == security.Encrypt(AuthPassword))
                return GetAccessToken();
            else
                return "false";
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database fundamental Function                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public Fundamental GetFundamental(string symbol = null, string year = null, string date = null)
        {
            var tmp = new Fundamental();
            if (symbol == null && year == null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly();
                tmp.finance_info_quarter = GetFinanceInfoQuarter();
                tmp.finance_stat_yearly = GetFinanceStatYearly();
                tmp.finance_stat_daily = GetFinanceStatDaily();
            }
            else if (symbol != null && year == null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol);
            }
            else if (symbol == null && year != null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(null, year);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(null, year);
                tmp.finance_stat_yearly = GetFinanceStatYearly(null, year);
                tmp.finance_stat_daily = GetFinanceStatDaily(null, year);
            }
            else if (symbol == null && year == null && date != null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(null, null, date);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(null, null, date);
                tmp.finance_stat_yearly = GetFinanceStatYearly(null, null, date);
                tmp.finance_stat_daily = GetFinanceStatDaily(null, null, date);
            }
            else if (symbol != null && year != null && date == null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol, year);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol, year);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol, year);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol, year);
            }
            else if (symbol != null && year == null && date != null)
            {
                tmp.finance_info_yearly = GetFinanceInfoYearly(symbol, null, date);
                tmp.finance_info_quarter = GetFinanceInfoQuarter(symbol, null, date);
                tmp.finance_stat_yearly = GetFinanceStatYearly(symbol, null, date);
                tmp.finance_stat_daily = GetFinanceStatDaily(symbol, null, date);
            }
            return tmp;
        }
        public List<FinanceInfo> GetFinanceInfoYearly(string symbol = null, string year = null, string date = null)
        {
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_yearly";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_yearly where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_yearly where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_yearly where Symbol = '{symbol}' AND Date = '{date}'";

            return SeleteDB<FinanceInfo>(sql);
        }
        public List<FinanceInfo> GetFinanceInfoQuarter(string symbol = null, string year = null, string date = null)
        {
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_quarter";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_quarter where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_quarter where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_info_quarter where Symbol = '{symbol}' AND Date = '{date}'";

            return SeleteDB<FinanceInfo>(sql);
        }
        public List<FinanceStat> GetFinanceStatYearly(string symbol = null, string year = null, string date = null)
        {
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_yearly where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_yearly where Symbol = '{symbol}' AND Date = '{date}'";


            return SeleteDB<FinanceStat>(sql);
        }
        public List<FinanceStat> GetFinanceStatDaily(string symbol = null, string year = null, string date = null)
        {
            string sql = "";
            if (symbol == null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_daily";
            else if (symbol != null && year == null && date == null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}'";
            else if (symbol == null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_daily where Year = '{year}'";
            else if (symbol == null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_daily where Date = '{date}'";
            else if (symbol != null && year != null && date == null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}' AND Year = '{year}'";
            else if (symbol != null && year == null && date != null)
                sql = $"Select * from dbo.finance_stat_daily where Symbol = '{symbol}' AND Date = '{date}'";

            return SeleteDB<FinanceStat>(sql);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database kaohoon Function                                       |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public Statement GetStatement(string symbol = null, bool last_update = false)
        {
            var tmp = new Statement();
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
            {
                tmp.asset = GetAssets(symbol);
                tmp.debt = GetDebt(symbol);
                tmp.share_holder_equity = GetShareHolderEquity(symbol);
                tmp.income = GetIncome(symbol);
                tmp.comprehensive_incomes = GetComprehensiveIncome(symbol);
                tmp.cash_flow = GetCashFlow(symbol);
            }
            else if (symbol != null && last_update)
            {
                tmp.asset = GetAssets(symbol, true);
                tmp.debt = GetDebt(symbol, true);
                tmp.share_holder_equity = GetShareHolderEquity(symbol, true);
                tmp.income = GetIncome(symbol, true);
                tmp.comprehensive_incomes = GetComprehensiveIncome(symbol, true);
                tmp.cash_flow = GetCashFlow(symbol, true);
            }
            else if (symbol == null && last_update)
            {
                tmp.asset = GetAssets(null, true);
                tmp.debt = GetDebt(null, true);
                tmp.share_holder_equity = GetShareHolderEquity(null, true);
                tmp.income = GetIncome(null, true);
                tmp.comprehensive_incomes = GetComprehensiveIncome(null, true);
                tmp.cash_flow = GetCashFlow(null, true);
            }
            else
            {
                tmp.asset = GetAssets();
                tmp.debt = GetDebt();
                tmp.share_holder_equity = GetShareHolderEquity();
                tmp.income = GetIncome();
                tmp.comprehensive_incomes = GetComprehensiveIncome();
                tmp.cash_flow = GetCashFlow();
            }
            return tmp;
        }
        public List<Assets> GetAssets(string symbol = null, bool last_update = false)
        {
            string sql = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
                sql = $"SELECT * from dbo.statement_asset WHERE Symbol = '{symbol}'";
            else if (symbol != null && last_update)
                sql = $"SELECT * from dbo.statement_asset WHERE Symbol = '{symbol}' AND Update_Date >= '{date}'";
            else if (symbol == null && last_update)
                sql = $"SELECT * from dbo.statement_asset WHERE Update_Date >= '{date}'";
            else
                sql = $"SELECT * from dbo.statement_asset";

            return SeleteDB<Assets>(sql);
        }
        public List<Debt> GetDebt(string symbol = null, bool last_update = false)
        {
            string sql = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
                sql = $"SELECT * from dbo.statement_debt WHERE Symbol = '{symbol}'";
            else if (symbol != null && last_update)
                sql = $"SELECT * from dbo.statement_debt WHERE Symbol = '{symbol}' AND Update_Date >= '{date}'";
            else if (symbol == null && last_update)
                sql = $"SELECT * from dbo.statement_debt WHERE Update_Date >= '{date}'";
            else
                sql = $"SELECT * from dbo.statement_debt";

            return SeleteDB<Debt>(sql);
        }
        public List<ShareHolderEquity> GetShareHolderEquity(string symbol = null, bool last_update = false)
        {
            string sql = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
                sql = $"SELECT * from dbo.statement_share_holder_equity WHERE Symbol = '{symbol}'";
            else if (symbol != null && last_update)
                sql = $"SELECT * from dbo.statement_share_holder_equity WHERE Symbol = '{symbol}' AND Update_Date >= '{date}'";
            else if (symbol == null && last_update)
                sql = $"SELECT * from dbo.statement_share_holder_equity WHERE Update_Date >= '{date}'";
            else
                sql = $"SELECT * from dbo.statement_share_holder_equity";

            return SeleteDB<ShareHolderEquity>(sql);
        }
        public List<Income> GetIncome(string symbol = null, bool last_update = false)
        {
            string sql = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
                sql = $"SELECT * from dbo.statement_income WHERE Symbol = '{symbol}'";
            else if (symbol != null && last_update)
                sql = $"SELECT * from dbo.statement_income WHERE Symbol = '{symbol}' AND Update_Date >= '{date}'";
            else if (symbol == null && last_update)
                sql = $"SELECT * from dbo.statement_income WHERE Update_Date >= '{date}'";
            else
                sql = $"SELECT * from dbo.statement_income";

            return SeleteDB<Income>(sql);
        }
        public List<ComprehensiveIncome> GetComprehensiveIncome(string symbol = null, bool last_update = false)
        {
            string sql = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
                sql = $"SELECT * from dbo.statement_comprehensive_income WHERE Symbol = '{symbol}'";
            else if (symbol != null && last_update)
                sql = $"SELECT * from dbo.statement_comprehensive_income WHERE Symbol = '{symbol}' AND Update_Date >= '{date}'";
            else if (symbol == null && last_update)
                sql = $"SELECT * from dbo.statement_comprehensive_income WHERE Update_Date >= '{date}'";
            else
                sql = $"SELECT * from dbo.statement_comprehensive_income";

            return SeleteDB<ComprehensiveIncome>(sql);
        }
        public List<CashFlow> GetCashFlow(string symbol = null, bool last_update = false)
        {
            string sql = "";
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            if (symbol != null && !last_update)
                sql = $"SELECT * from dbo.statement_cash_flow WHERE Symbol = '{symbol}'";
            else if (symbol != null && last_update)
                sql = $"SELECT * from dbo.statement_cash_flow WHERE Symbol = '{symbol}' AND Update_Date >= '{date}'";
            else if (symbol == null && last_update)
                sql = $"SELECT * from dbo.statement_cash_flow WHERE Update_Date >= '{date}'";
            else
                sql = $"SELECT * from dbo.statement_cash_flow";

            return SeleteDB<CashFlow>(sql);
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database kaohoon Function                                       |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public List<KaohoonData> GetKaohoonData(string symbol = null, string date = null)
        {
            List<KaohoonData> kaohoon_data = new List<KaohoonData>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            string sql = "";
            if (symbol == null && date == null)
                sql = $"Select * from dbo.kaohoon_data";
            else if (symbol != null && date == null)
                sql = $"Select * from dbo.kaohoon_data where Symbol = '{symbol}'";
            else if (symbol == null && date != null)
                sql = $"Select * from dbo.kaohoon_data where Date = '{date}'";
            else
                sql = $"Select * from dbo.kaohoon_data where Symbol = '{symbol}' AND Date = '{date}'";

            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new KaohoonData
                    {
                        Kaohoon_data_id = String.Format("{0}", reader["Kaohoon_data_id"]),
                        Symbol = String.Format("{0}", reader["Symbol"]),
                        Date = String.Format("{0}", reader["Date"]),
                        Year = String.Format("{0}", reader["Year"]),
                        Quarter = String.Format("{0}", reader["Quarter"]),
                        TargetPrice = String.Format("{0}", reader["TargetPrice"]),
                        Trends = String.Format("{0}", reader["Trends"]),
                        Divide = String.Format("{0}", reader["Divide"]),
                        Topic = String.Format("{0}", reader["Topic"]),
                    };
                    kaohoon_data.Add(tmp);
                }
            }
            return kaohoon_data;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database                                                        |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        static public dynamic SeleteDB<T>(string sql) where T : class, new()
        {
            List<T> item = new List<T>();
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tmp = new T();
                    foreach (var propertyInfo in tmp.GetType().GetProperties())
                    {
                        var key = propertyInfo.Name;
                        var prop = tmp.GetType().GetProperty(key);
                        prop.SetValue(tmp, String.Format("{0}", reader[key]), null);
                    }
                    item.Add(tmp);
                }
            }
            cnn.Close();

            return item;
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other Function                                                  |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void SetAccessToken(string input)
        {
            accessToken = input;
        }
        public string GetAccessToken()
        {
            return accessToken;
        }
    }
}

