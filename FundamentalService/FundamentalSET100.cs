using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FundamentalService
{
    class FundamentalSET100
    {
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Config                                                          |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        private static string DatabaseServer = ConfigurationManager.AppSettings["DatabaseServer"];
        private static string Database = ConfigurationManager.AppSettings["Database"];
        private static string Username = ConfigurationManager.AppSettings["DatabaseUsername"];
        private static string Password = ConfigurationManager.AppSettings["DatabasePassword"];
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
            public string Assets { get; set; }
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
        public class IAAConsensus
        {

            public IAAConsensus() { }

            // Properties.
            public string Symbol { get; set; }
            public string Broker { get; set; }
            public string EPS_Year { get; set; }
            public string EPS_Year_Change { get; set; }
            public string EPS_2Year { get; set; }
            public string EPS_2Year_Change { get; set; }
            public string PE { get; set; }
            public string PBV { get; set; }
            public string DIV { get; set; }
            public string TargetPrice { get; set; }
            public string Rec { get; set; }
            public string LastUpdate { get; set; }
        }
        public class IAAConsensusSummary
        {

            public IAAConsensusSummary() { }

            // Properties.
            public string Symbol { get; set; }
            public string LastPrice { get; set; }
            public string Buy { get; set; }
            public string Hold { get; set; }
            public string Sell { get; set; }
            public string Average_EPS_Year { get; set; }
            public string Average_EPS_Year_Change { get; set; }
            public string Average_EPS_2Year { get; set; }
            public string Average_EPS_2Year_Change { get; set; }
            public string Average_PE { get; set; }
            public string Average_PBV { get; set; }
            public string Average_DIV { get; set; }
            public string Average_TargetPrice { get; set; }
            public string High_EPS_Year { get; set; }
            public string High_EPS_Year_Change { get; set; }
            public string High_EPS_2Year { get; set; }
            public string High_EPS_2Year_Change { get; set; }
            public string High_PE { get; set; }
            public string High_PBV { get; set; }
            public string High_DIV { get; set; }
            public string High_TargetPrice { get; set; }
            public string Low_EPS_Year { get; set; }
            public string Low_EPS_Year_Change { get; set; }
            public string Low_EPS_2Year { get; set; }
            public string Low_EPS_2Year_Change { get; set; }
            public string Low_PE { get; set; }
            public string Low_PBV { get; set; }
            public string Low_DIV { get; set; }
            public string Low_TargetPrice { get; set; }
            public string Median_EPS_Year { get; set; }
            public string Median_EPS_Year_Change { get; set; }
            public string Median_EPS_2Year { get; set; }
            public string Median_EPS_2Year_Change { get; set; }
            public string Median_PE { get; set; }
            public string Median_PBV { get; set; }
            public string Median_DIV { get; set; }
            public string Median_TargetPrice { get; set; }
            public string LastUpdate { get; set; }
        }
        public class RightsBenefits
        {

            public RightsBenefits() { }

            // Properties.
            public string Symbol { get; set; }
            public string X_Date { get; set; }
            public string Sign { get; set; }
            public string Book_Close_Date { get; set; }
            public string Record_Date { get; set; }
            public string Meeting_Date { get; set; }
            public string Agenda { get; set; }
            public string Type_of_Meeting { get; set; }
            public string Venue { get; set; }
            public string Payment_Date { get; set; }
            public string Dividend_per_Share { get; set; }
            public string Operation_Period { get; set; }
            public string Source_of_Dividend { get; set; }
            public string Remark { get; set; }
        }
        public class News
        {

            public News() { }

            // Properties.
            public string Symbol { get; set; }
            public string DateTime { get; set; }
            public string Source { get; set; }
            public string Headline { get; set; }
            public string Link { get; set; }
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Variable                                                        |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        static string main = "", sub = "", nano = "", last = "";
        static Assets asset;
        static Debt debt;
        static ShareHolderEquity share_holder_equity;
        static Income income;
        static ComprehensiveIncome comprehensive_income;
        static CashFlow cash_flow;
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Main Function                                                   |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public void Run()
        {
            var url1 = $"https://marketdata.set.or.th/mkt/sectorquotation.do?sector=SET100&language=th&country=TH";
            List<string> symbols = new List<string>();
            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url1);

            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//td//a"))
            {
                string utf8_String = node.InnerText;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                utf8_String = utf8_String.Replace(" ", String.Empty);
                if (utf8_String.IndexOf("\n") >= 0)
                {
                    utf8_String = utf8_String.Substring(2, utf8_String.Length - 4);
                    symbols.Add(utf8_String);
                }

            }

            for (var i = 0; i < symbols.Count; i++)
            {
                CompanyHighlights(symbols[i]);
                StatementOfFinancialPosition(symbols[i]);
                StatementOfComprehensiveIncome(symbols[i]);
                StatementOfCashFlow(symbols[i]);
                RightsBenefitsScraping(symbols[i]);
                IAAConsensusScraping(symbols[i]);
            }
            log.LOGI("[FundamentalSET100::Run] Success update data SET100");
        }
        public void RunNews()
        {
            var url1 = $"https://marketdata.set.or.th/mkt/sectorquotation.do?sector=SET100&language=th&country=TH";
            List<string> symbols = new List<string>();
            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url1);

            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//td//a"))
            {
                string utf8_String = node.InnerText;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                utf8_String = utf8_String.Replace(" ", String.Empty);
                if (utf8_String.IndexOf("\n") >= 0)
                {
                    utf8_String = utf8_String.Substring(2, utf8_String.Length - 4);
                    symbols.Add(utf8_String);
                }
            }

            for (var i = 0; i < symbols.Count; i++)
            {
                NewsScraping(symbols[i]);
            }
            log.LOGI("[FundamentalSET100::RunNews] Success update data SET100 News");
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | ScrapingWeb Function                                            |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static void CompanyHighlights(string symbol)
        {
            var url = $"https://www.set.or.th/set/companyhighlight.do?symbol={symbol}&ssoPageId=5";

            // List cut string finance info
            List<string> date_info = new List<string>();
            List<string> year_info = new List<string>();
            List<string> quarter = new List<string>();
            List<string> asset = new List<string>();
            List<string> liabilities = new List<string>();
            List<string> equity = new List<string>();
            List<string> paid_up_cap = new List<string>();
            List<string> revenue = new List<string>();
            List<string> net_profit = new List<string>();
            List<string> eps = new List<string>();
            List<string> roa = new List<string>();
            List<string> roe = new List<string>();
            List<string> net_profit_margin = new List<string>();

            // List cut string finance stat
            List<string> date_stat = new List<string>();
            List<string> year_stat = new List<string>();
            List<string> lastprice = new List<string>();
            List<string> market_cap = new List<string>();
            List<string> fs_date = new List<string>();
            List<string> pe = new List<string>();
            List<string> pbv = new List<string>();
            List<string> book_value_share = new List<string>();
            List<string> dvd_yield = new List<string>();

            // Using HtmlAgilityPack
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);

            // tmp variable
            var run = "start";

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr//td"))
            {
                string utf8_String = node.ChildNodes[0].InnerHtml;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                int index = utf8_String.IndexOf("&");

                if (index > 0)
                    utf8_String = utf8_String.Substring(0, utf8_String.IndexOf("&"));

                if (utf8_String == "N/A" || utf8_String == "N.A." || utf8_String == "-")
                    utf8_String = "null";

                if (utf8_String == "สินทรัพย์รวม")
                    run = utf8_String;
                else if (utf8_String == "หนี้สินรวม")
                    run = utf8_String;
                else if (utf8_String == "ส่วนของผู้ถือหุ้น")
                    run = utf8_String;
                else if (utf8_String == "มูลค่าหุ้นที่เรียกชำระแล้ว")
                    run = utf8_String;
                else if (utf8_String == "รายได้รวม")
                    run = utf8_String;
                else if (utf8_String == "กำไรสุทธิ")
                    run = utf8_String;
                else if (utf8_String == "กำไรต่อหุ้น (บาท)")
                    run = utf8_String;
                else if (utf8_String == "ROA(%)")
                    run = utf8_String;
                else if (utf8_String == "ROE(%)")
                    run = utf8_String;
                else if (utf8_String == "อัตรากำไรสุทธิ(%)")
                    run = utf8_String;
                else if (utf8_String == "ราคาล่าสุด(บาท)")
                    run = utf8_String;
                else if (utf8_String == "มูลค่าหลักทรัพย์ตามราคาตลาด")
                    run = utf8_String;
                else if (utf8_String == "วันที่ของงบการเงินที่ใช้คำนวณค่าสถิติ")
                    run = utf8_String;
                else if (utf8_String == "P/E (เท่า)")
                    run = utf8_String;
                else if (utf8_String == "P/BV (เท่า)")
                    run = utf8_String;
                else if (utf8_String == "มูลค่าหุ้นทางบัญชีต่อหุ้น (บาท)")
                    run = utf8_String;
                else if (utf8_String == "อัตราส่วนเงินปันผลตอบแทน(%)")
                    run = utf8_String;

                if (utf8_String != run)
                    if (run == "สินทรัพย์รวม")
                        asset.Add(utf8_String);
                    else if (run == "หนี้สินรวม")
                        liabilities.Add(utf8_String);
                    else if (run == "ส่วนของผู้ถือหุ้น")
                        equity.Add(utf8_String);
                    else if (run == "มูลค่าหุ้นที่เรียกชำระแล้ว")
                        paid_up_cap.Add(utf8_String);
                    else if (run == "รายได้รวม")
                        revenue.Add(utf8_String);
                    else if (run == "กำไรสุทธิ")
                        net_profit.Add(utf8_String);
                    else if (run == "กำไรต่อหุ้น (บาท)")
                        eps.Add(utf8_String);
                    else if (run == "ROA(%)")
                        roa.Add(utf8_String);
                    else if (run == "ROE(%)")
                        roe.Add(utf8_String);
                    else if (run == "อัตรากำไรสุทธิ(%)")
                        net_profit_margin.Add(utf8_String);
                    else if (run == "ราคาล่าสุด(บาท)")
                        lastprice.Add(utf8_String);
                    else if (run == "มูลค่าหลักทรัพย์ตามราคาตลาด")
                        market_cap.Add(utf8_String);
                    else if (run == "วันที่ของงบการเงินที่ใช้คำนวณค่าสถิติ")
                        fs_date.Add(utf8_String);
                    else if (run == "P/E (เท่า)")
                        pe.Add(utf8_String);
                    else if (run == "P/BV (เท่า)")
                        pbv.Add(utf8_String);
                    else if (run == "มูลค่าหุ้นทางบัญชีต่อหุ้น (บาท)")
                        book_value_share.Add(utf8_String);
                    else if (run == "อัตราส่วนเงินปันผลตอบแทน(%)")
                        dvd_yield.Add(utf8_String);
            }

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//th"))
            {
                string utf8_String = node.ChildNodes[0].InnerHtml;
                byte[] bytes = Encoding.UTF8.GetBytes(utf8_String);
                utf8_String = Encoding.UTF8.GetString(bytes);
                if (utf8_String == "งวดงบการเงิน<br> ณ วันที่")
                    run = utf8_String;
                if (utf8_String == "ค่าสถิติสำคัญ<br> ณ วันที่")
                    run = utf8_String;

                if (utf8_String != run)
                    if (run == "งวดงบการเงิน<br> ณ วันที่" && utf8_String.IndexOf("/") >= 0)
                    {
                        var index = utf8_String.IndexOf(">") + 1;
                        date_info.Add(utf8_String.Substring(index, utf8_String.Length - index));
                        year_info.Add(utf8_String.Substring(utf8_String.Length - 4, 4));
                        quarter.Add(utf8_String.Substring(0, index - 4));
                    }
                    else if (run == "ค่าสถิติสำคัญ<br> ณ วันที่")
                    {
                        date_stat.Add(utf8_String);
                        year_stat.Add(utf8_String.Substring(utf8_String.Length - 4, 4));
                    }

            }

            List<FinanceInfo> finance_info_yearly = new List<FinanceInfo>();
            List<FinanceInfo> finance_info_quarter = new List<FinanceInfo>();
            List<FinanceStat> finance_stat_yearly = new List<FinanceStat>();
            List<FinanceStat> finance_stat_daily = new List<FinanceStat>();

            for (int i = 0; i < date_stat.Count; i++)
            {
                string fsDate = "";
                if (fs_date[i].IndexOf("/") >= 0)
                    fsDate = fs_date[i];
                else
                    fsDate = null;
                var tmp1 = new FinanceStat
                {
                    Date = ChangeDateFormat(date_stat[i]),
                    Symbol = symbol,
                    Year = ChangeYearFormat(year_stat[i]),
                    Lastprice = CutStrignMoney(lastprice[i]),
                    Market_cap = CutStrignMoney(market_cap[i]),
                    FS_date = ChangeDateFormat(fsDate),
                    PE = CutStrignMoney(pe[i]),
                    PBV = CutStrignMoney(pbv[i]),
                    BookValue_Share = CutStrignMoney(book_value_share[i]),
                    Dvd_Yield = CutStrignMoney(dvd_yield[i]),
                };
                if (i < date_stat.Count - 1)
                {
                    finance_stat_yearly.Add(tmp1);
                    var tmp = new FinanceInfo
                    {
                        Date = ChangeDateFormat(date_info[i]),
                        Symbol = symbol,
                        Year = ChangeYearFormat(year_info[i]),
                        Quarter = quarter[i],
                        Assets = CutStrignMoney(asset[i]),
                        Liabilities = CutStrignMoney(liabilities[i]),
                        Equity = CutStrignMoney(equity[i]),
                        Paid_up_cap = CutStrignMoney(paid_up_cap[i]),
                        Revenue = CutStrignMoney(revenue[i]),
                        NetProfit = CutStrignMoney(net_profit[i]),
                        EPS = CutStrignMoney(eps[i]),
                        ROA = CutStrignMoney(roa[i]),
                        ROE = CutStrignMoney(roe[i]),
                        NetProfitMargin = CutStrignMoney(net_profit_margin[i]),
                    };
                    if (tmp.Quarter.IndexOf("ไตรมาส") >= 0)
                    {
                        tmp.Quarter = quarter[i].Substring(quarter[i].IndexOf("ไตรมาส") + 6, 1);
                        finance_info_quarter.Add(tmp);
                    }
                    else
                    {
                        tmp.Quarter = null;
                        finance_info_yearly.Add(tmp);
                    }
                }
                else
                {
                    finance_stat_daily.Add(tmp1);
                }

            }

            foreach (var value in finance_info_yearly)
            {
                // Insert or Update datebase finance_info_yearly
                StatementDatabase(value, "finance_info_yearly", $"Date='{value.Date}' AND Symbol='{value.Symbol}'");
            }

            foreach (var value in finance_info_quarter)
            {
                // Insert or Update datebase finance_info_quarter
                StatementDatabase(value, "finance_info_quarter", $"Date='{value.Date}' AND Symbol='{value.Symbol}'");
            }

            foreach (var value in finance_stat_yearly)
            {
                // Insert or Update datebase finance_stat_yearly
                StatementDatabase(value, "finance_stat_yearly", $"Date='{value.Date}' AND Symbol='{value.Symbol}'");
            }

            foreach (var value in finance_stat_daily)
            {
                // Insert or Update datebase finance_stat_daily
                StatementDatabase(value, "finance_stat_daily", $"Date='{value.Date}' AND Symbol='{value.Symbol}'");
            }

            GC.Collect();

        }
        public static void StatementOfFinancialPosition(string symbol)
        {
            var url = $"https://www.set.or.th/set/companyfinance.do?symbol={symbol}";
            // Using HtmlAgilityPack
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);

            // Set values
            asset = new Assets();
            debt = new Debt();
            share_holder_equity = new ShareHolderEquity();

            string check_case = "";
            int number = 0;
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr"))
            {
                var cells = node.SelectNodes("td[@height='18']");
                if (cells != null && number++ == 2)
                {
                    asset.Year = cells[1].InnerText;
                    debt.Year = cells[1].InnerText;
                    share_holder_equity.Year = cells[1].InnerText;
                    asset.Symbol = symbol;
                    debt.Symbol = symbol;
                    share_holder_equity.Symbol = symbol;
                    asset.Update_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    debt.Update_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    share_holder_equity.Update_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='table-responsive']//tr"))
            {
                var cells = node.SelectNodes("td");
                if (cells.Count < 3)
                {
                    if (cells[1].InnerText == "สินทรัพย์")
                        check_case = "สินทรัพย์";
                    else if (cells[1].InnerText == "หนี้สิน")
                        check_case = "หนี้สิน";
                    else if (cells[1].InnerText == "ส่วนของผู้ถือหุ้น")
                        check_case = "ส่วนของผู้ถือหุ้น";
                }
                else
                {
                    var name = cells[1].InnerText;
                    var value = CutStrignMoney(cells[2].InnerText);
                    if (check_case == "สินทรัพย์")
                        AssetCutstring(name, value);
                    else if (check_case == "หนี้สิน")
                        DebtCutstring(name, value);
                    else if (check_case == "ส่วนของผู้ถือหุ้น")
                        ShareHolderEquityCutstring(name, value);
                }
            }

            // Insert or Update datebase asset
            StatementDatabase(asset, "statement_asset", $"Year='{asset.Year}' AND Symbol='{asset.Symbol}'");

            // Insert or Update datebase debt
            StatementDatabase(debt, "statement_debt", $"Year='{debt.Year}' AND Symbol='{debt.Symbol}'");

            // Insert or Update datebase share_holder_equity
            StatementDatabase(share_holder_equity, "statement_share_holder_equity", $"Year='{share_holder_equity.Year}' AND Symbol='{share_holder_equity.Symbol}'");

            GC.Collect();
        }
        public static void StatementOfComprehensiveIncome(string symbol)
        {
            var url = $"https://www.set.or.th/set/companyfinance.do?type=income&symbol={symbol}";
            // Using HtmlAgilityPack
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);

            // Set values
            income = new Income();
            comprehensive_income = new ComprehensiveIncome();

            string check_case = "";
            int number = 0;
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr"))
            {
                var cells = node.SelectNodes("td[@height='18']");
                if (cells != null && number++ == 2)
                {
                    income.Year = cells[1].InnerText;
                    comprehensive_income.Year = cells[1].InnerText;
                    income.Symbol = symbol;
                    comprehensive_income.Symbol = symbol;
                    income.Update_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    comprehensive_income.Update_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='table-responsive']//tr"))
            {
                var cells = node.SelectNodes("td");
                if (cells.Count < 3)
                    check_case = cells[1].InnerText;
                else
                {
                    var name = cells[1].InnerText;
                    var value = CutStrignMoney(cells[2].InnerText);
                    if (check_case == "งบกำไรขาดทุน")
                        IncomeCutstring(name, value);
                    else if (check_case == "งบกำไรขาดทุนเบ็ดเสร็จอื่น")
                        ComprehensiveIncomeCutstring(name, value);
                }

            }

            // Insert or Update datebase income
            StatementDatabase(income, "statement_income", $"Year='{income.Year}' AND Symbol='{income.Symbol}'");

            // Insert or Update datebase comprehensive_income
            StatementDatabase(comprehensive_income, "statement_comprehensive_income", $"Year='{comprehensive_income.Year}' AND Symbol='{comprehensive_income.Symbol}'");

            GC.Collect();
        }
        public static void StatementOfCashFlow(string symbol)
        {
            var url = $"https://www.set.or.th/set/companyfinance.do?type=cashflow&symbol={symbol}";
            // Using HtmlAgilityPack
            var Webget = new HtmlWeb();
            var doc = Webget.Load(url);

            // Set values
            cash_flow = new CashFlow();

            string check_case = "";
            int number = 0;
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//tr"))
            {
                var cells = node.SelectNodes("td[@height='18']");
                if (cells != null && number++ == 2)
                {
                    cash_flow.Year = cells[1].InnerText;
                    cash_flow.Symbol = symbol;
                    cash_flow.Update_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//div[@class='table-responsive']//tr"))
            {
                var cells = node.SelectNodes("td");
                if (cells.Count < 3)
                    check_case = cells[1].InnerText;
                else
                {
                    var name = cells[1].InnerText;
                    var value = CutStrignMoney(cells[2].InnerText);
                    if (check_case == "งบกระแสเงินสด")
                        CashFlowCutstring(name, value);
                }

            }

            // Insert or Update datebase cash_flow
            StatementDatabase(cash_flow, "statement_cash_flow", $"Year='{cash_flow.Year}' AND Symbol='{cash_flow.Symbol}'");

            GC.Collect();
        }
        static public void IAAConsensusScraping(string symbol)
        {

            var url = $"https://www.settrade.com/AnalystConsensus/C04_10_stock_saa_p1.jsp?txtSymbol={symbol}&ssoPageId=11&selectPage=10";

            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url);

            IAAConsensusSummary iaa_consensus_summary = new IAAConsensusSummary();
            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//div[@class='round-border']"))
            {
                iaa_consensus_summary.Symbol = symbol;
                iaa_consensus_summary.LastPrice = node.SelectSingleNode(".//div[@class='col-xs-8']//div[@class='text-right']//h1").InnerText;
                string result = "";
                foreach (HtmlNode row in node.SelectNodes(".//div[@class='row separate-content']//div[@class='row']"))
                {
                    foreach (HtmlNode cell in row.SelectNodes("div"))
                        result += $"{cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "")} ";
                }
                var parts = result.Split(' ');
                iaa_consensus_summary.Buy = parts[1];
                iaa_consensus_summary.Hold = parts[4];
                iaa_consensus_summary.Sell = parts[7];
                iaa_consensus_summary.LastUpdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            List<IAAConsensus> iaa_consensus = new List<IAAConsensus>();
            var index = 1;
            foreach (HtmlNode row in doc1.DocumentNode.SelectNodes("//tr"))
            {
                string result = "";
                foreach (HtmlNode cell in row.SelectNodes("th|td"))
                {
                    result += $"{cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace(" ", "")} ";
                }
                var parts = result.Split(' ');
                if (index.ToString() == parts[0])
                {
                    var tmp = new IAAConsensus();
                    tmp.Symbol = symbol;
                    tmp.Broker = parts[1];
                    tmp.EPS_Year = parts[2] == "-" ? null : CutStrignMoney(parts[2]);
                    tmp.EPS_Year_Change = parts[3] == "-" ? null : CutStrignMoney(parts[3]);
                    tmp.EPS_2Year = parts[4] == "-" ? null : CutStrignMoney(parts[4]);
                    tmp.EPS_2Year_Change = parts[5] == "-" ? null : CutStrignMoney(parts[5]);
                    tmp.PE = parts[6] == "-" ? null : CutStrignMoney(parts[6]);
                    tmp.PBV = parts[7] == "-" ? null : CutStrignMoney(parts[7]);
                    tmp.DIV = parts[8] == "-" ? null : CutStrignMoney(parts[8]);
                    tmp.TargetPrice = parts[9] == "-" ? null : CutStrignMoney(parts[9]);
                    tmp.Rec = parts[10];
                    tmp.LastUpdate = ChangeDateFormat2(parts[11]);
                    index++;
                    iaa_consensus.Add(tmp);
                }
                else if (parts[0] == "Average")
                {
                    iaa_consensus_summary.Average_EPS_Year = parts[1] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_EPS_Year_Change = parts[2] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_EPS_2Year = parts[3] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_EPS_2Year_Change = parts[4] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_PE = parts[5] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_PBV = parts[6] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_DIV = parts[7] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Average_TargetPrice = parts[8] == "-" ? null : CutStrignMoney(parts[1]);
                }
                else if (parts[0] == "High")
                {
                    iaa_consensus_summary.High_EPS_Year = parts[1] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_EPS_Year_Change = parts[2] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_EPS_2Year = parts[3] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_EPS_2Year_Change = parts[4] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_PE = parts[5] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_PBV = parts[6] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_DIV = parts[7] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.High_TargetPrice = parts[8] == "-" ? null : CutStrignMoney(parts[1]);
                }
                else if (parts[0] == "Low")
                {
                    iaa_consensus_summary.Low_EPS_Year = parts[1] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_EPS_Year_Change = parts[2] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_EPS_2Year = parts[3] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_EPS_2Year_Change = parts[4] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_PE = parts[5] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_PBV = parts[6] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_DIV = parts[7] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Low_TargetPrice = parts[8] == "-" ? null : CutStrignMoney(parts[1]);
                }
                else if (parts[0] == "Median")
                {
                    iaa_consensus_summary.Median_EPS_Year = parts[1] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_EPS_Year_Change = parts[2] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_EPS_2Year = parts[3] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_EPS_2Year_Change = parts[4] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_PE = parts[5] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_PBV = parts[6] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_DIV = parts[7] == "-" ? null : CutStrignMoney(parts[1]);
                    iaa_consensus_summary.Median_TargetPrice = parts[8] == "-" ? null : CutStrignMoney(parts[1]);
                }

            }

            foreach (var value in iaa_consensus)
            {
                // Insert or Update datebase iaa_consensus
                StatementDatabase(value, "iaa_consensus", $"LastUpdate='{value.LastUpdate}' AND Symbol='{value.Symbol}' AND Broker='{value.Broker}'");
            }

            // Insert or Update datebase iaa_consensus_summary
            StatementDatabase(iaa_consensus_summary, "iaa_consensus_summary", $"Symbol='{iaa_consensus_summary.Symbol}'");


            GC.Collect();
        }
        static public void RightsBenefitsScraping(string symbol)
        {

            var url = $"https://www.set.or.th/set/companyrights.do?symbol={symbol}&ssoPageId=7&language=th&country=TH";

            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url);

            RightsBenefits result = new RightsBenefits();
            List<RightsBenefits> rights_benefits = new List<RightsBenefits>();
            foreach (HtmlNode row in doc1.DocumentNode.SelectNodes("//table[@class='table table-hover table-info-wrap']//tbody//tr"))
            {
                string tmp = "";
                if (row.ChildNodes.Count > 6)
                {
                    if (result.X_Date != null)
                        rights_benefits.Add(result);
                    result = new RightsBenefits();
                    foreach (HtmlNode cell in row.SelectNodes("td"))
                        tmp += $"{cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "")},";
                    var parts = tmp.Split(',');
                    result.Symbol = symbol;
                    result.X_Date = ChangeDateFormat3(parts[0]);
                    result.Sign = parts[1].Replace(" ", "");
                }
                else if (row.ChildNodes.Count < 6)
                {
                    foreach (HtmlNode cell in row.SelectNodes("td"))
                        tmp += $"{cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "")},";
                    var parts = tmp.Split(',');
                    if (parts[0] == "วันกำหนดรายชื่อผู้ถือหุ้น")
                        result.Record_Date = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : ChangeDateFormat3(parts[1]);
                    else if (parts[0] == "วันที่ประชุม")
                        result.Meeting_Date = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : ChangeDateFormat3(parts[1]);
                    else if (parts[0] == "วาระการประชุม")
                        result.Agenda = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : parts[1];
                    else if (parts[0] == "ประเภทของการประชุม")
                        result.Type_of_Meeting = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : parts[1];
                    else if (parts[0] == "สถานที่จัดการประชุม")
                        result.Venue = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : parts[1];
                    else if (parts[0] == "วันปิดสมุดทะเบียน")
                        result.Book_Close_Date = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : ChangeDateFormat3(parts[1]);
                    else if (parts[0] == "วันจ่ายปันผล")
                        result.Payment_Date = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : ChangeDateFormat3(parts[1]);
                    else if (parts[0] == "เงินปันผล(บาท/หุ้น)")
                        result.Dividend_per_Share = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : CutStrignMoney(parts[1]);
                    else if (parts[0] == "รอบผลประกอบการ")
                        result.Operation_Period = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : parts[1];
                    else if (parts[0] == "เงินปันผลจาก")
                        result.Source_of_Dividend = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : parts[1];
                    else if (parts[0] == "หมายเหตุ")
                        result.Remark = (parts[1] == "-" || parts[1] == "" || parts[1] == " - ") ? null : parts[1];

                }

            }

            foreach (var value in rights_benefits)
            {
                // Insert or Update datebase rights_benefits
                StatementDatabase(value, "rights_benefits", $"X_Date='{value.X_Date}' AND Symbol='{value.Symbol}' AND Sign='{value.Sign}'");
            }


            GC.Collect();
        }
        static public void NewsScraping(string symbol)
        {

            var url = $"https://www.set.or.th/set/companynews.do?symbol={symbol}&language=th&currentpage=0&ssoPageId=8&country=TH";

            // Using HtmlAgilityPack
            var Webget1 = new HtmlWeb();
            var doc1 = Webget1.Load(url);

            News result = new News();
            List<News> news = new List<News>();
            int index = 0;
            foreach (HtmlNode node in doc1.DocumentNode.SelectNodes("//table[@class='table table-hover table-info-wrap']"))
            {
                if (index++ == 1)
                    foreach (HtmlNode row in node.SelectNodes(".//tbody//tr"))
                    {
                        result = new News();
                        result.Symbol = symbol;
                        index = 0;
                        foreach (HtmlNode cell in row.SelectNodes(".//td"))
                        {
                            if (index == 0)
                                result.DateTime = ChangeDateFormat3(cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("  ", ""));
                            else if (index == 2)
                                result.Source = cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                            else if (index == 3)
                                result.Headline = cell.InnerText.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
                            else if (index == 4)
                                result.Link = "https://www.set.or.th" + cell.SelectSingleNode(".//a[@href]").GetAttributeValue("href", string.Empty);
                            index++;
                        }
                        news.Add(result);
                    }

            }

            foreach (var value in news)
            {
                // Insert or Update datebase news
                StatementDatabase(value, "news", $"DateTime='{value.DateTime}' AND Symbol='{value.Symbol}' AND Headline='{value.Headline}'", true);
            }


            GC.Collect();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Database Function                                               |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static void UpdateDatebase(string sql, SqlConnection cnn)
        {
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();


            command = new SqlCommand(sql, cnn);

            adapter.UpdateCommand = new SqlCommand(sql, cnn);
            adapter.UpdateCommand.ExecuteNonQuery();

            command.Dispose();
        }
        public static void InsertDatebase(string sql, SqlConnection cnn)
        {
            SqlCommand command;
            SqlDataAdapter adapter = new SqlDataAdapter();


            command = new SqlCommand(sql, cnn);

            adapter.InsertCommand = new SqlCommand(sql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            command.Dispose();
        }
        public static void StatementDatabase(object item, string db, string where, bool none_update = false)
        {
            string sql = "";
            string connetionString;
            SqlConnection cnn;
            connetionString = $@"Data Source={DatabaseServer};Initial Catalog={Database};User ID={Username};Password={Password}";
            cnn = new SqlConnection(connetionString);
            cnn.Open();

            sql = $"Select * from dbo.{db} where {where}";
            SqlCommand command = new SqlCommand(sql, cnn);
            command.Parameters.AddWithValue("@zip", "india");
            bool event_case = true;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    sql = GetUpdateSQL(item, db, where);
                    event_case = false;
                }
                else
                {
                    //Insert share_holder_equity
                    sql = GetInsertSQL(item, db);
                }
            }
            if (event_case)
                InsertDatebase(sql, cnn);
            else if (!none_update && !event_case)
                UpdateDatebase(sql, cnn);

            cnn.Close();
        }
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        // | Other    Function                                               |
        // = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = =
        public static string CutStrignMoney(string money)
        {
            if (money == "null")
                return "0";
            else
                return money.Replace(",", "").Replace("*", "");
        }
        public static string ChangeDateFormat(string date)
        {
            if (date == null)
                return date;

            string str = date + "/";
            var parts = str.Split('/');
            int dd = Convert.ToInt32(parts[0]);
            int mm = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]) - 543;

            return $"{yy}-{mm}-{dd}";
        }
        public static string ChangeDateFormat2(string date)
        {
            if (date == null)
                return date;

            string str = date + "/";
            var parts = str.Split('/');
            int dd = Convert.ToInt32(parts[0]);
            int mm = Convert.ToInt32(parts[1]);
            int yy = Convert.ToInt32(parts[2]) + 2000;

            return $"{yy}-{mm}-{dd}";
        }
        public static string ChangeDateFormat3(string date)
        {
            if (date == "null")
                return date;

            var parts = date.Split(' ');
            int mm;
            switch (parts[1])
            {
                case "ม.ค.":
                    mm = 1;
                    break;
                case "ก.พ.":
                    mm = 2;
                    break;
                case "มี.ค.":
                    mm = 3;
                    break;
                case "เม.ย.":
                    mm = 4;
                    break;
                case "พ.ค.":
                    mm = 5;
                    break;
                case "มิ.ย.":
                    mm = 6;
                    break;
                case "ก.ค.":
                    mm = 7;
                    break;
                case "ส.ค.":
                    mm = 8;
                    break;
                case "ก.ย.":
                    mm = 9;
                    break;
                case "ต.ค.":
                    mm = 10;
                    break;
                case "พ.ย.":
                    mm = 11;
                    break;
                default:
                    mm = 12;
                    break;
            }
            int dd = Convert.ToInt32(parts[0]);
            int yy = Convert.ToInt32(parts[2]) - 543;

            if (parts.Length > 3)
                return $"{yy}-{mm}-{dd} {parts[3]}:00.000";

            else

                return $"{yy}-{mm}-{dd}";
        }
        public static string ChangeYearFormat(string year)
        {
            int yy = Convert.ToInt32(year) - 543;
            return $"{yy}";
        }
        public static string GetInsertSQL(object item, string db)
        {
            string sql = $"INSERT INTO dbo.{db} (:columns:) VALUES (:values:);";

            string[] columns = new string[item.GetType().GetProperties().Count()];
            string[] values = new string[item.GetType().GetProperties().Count()];
            int i = 0;
            foreach (var propertyInfo in item.GetType().GetProperties())
            {
                columns[i] = propertyInfo.Name;
                values[i++] = (string)(propertyInfo.GetValue(item, null));
            }

            //replacing the markers with the desired column names and values
            sql = FillColumnsAndValuesIntoInsertQuery(sql, columns, values);

            return sql;
        }
        public static string GetUpdateSQL(object item, string db, string whare)
        {
            string sql = $"UPDATE dbo.{db} SET :update: WHERE {whare} ;";

            string[] columns = new string[item.GetType().GetProperties().Count()];
            string[] values = new string[item.GetType().GetProperties().Count()];
            int i = 0;
            foreach (var propertyInfo in item.GetType().GetProperties())
            {
                columns[i] = propertyInfo.Name;
                values[i++] = (string)(propertyInfo.GetValue(item, null));
            }

            //replacing the markers with the desired column names and values
            sql = FillColumnsAndValuesIntoUpdateQuery(sql, columns, values);

            return sql;
        }
        public static string FillColumnsAndValuesIntoInsertQuery(string query, string[] columns, string[] values)
        {
            //joining the string arrays with a comma character
            string columnnames = string.Join(",", columns);
            //adding values with single quotation marks around them to handle errors related to string values
            string valuenames = ("'" + string.Join("','", values) + "'").Replace("''", "null");
            //replacing the markers with the desired column names and values
            return query.Replace(":columns:", columnnames).Replace(":values:", valuenames);
        }
        public static string FillColumnsAndValuesIntoUpdateQuery(string query, string[] columns, string[] values)
        {
            string result = "";
            for (int i = 0; i < columns.Length; i++)
                if (values[i] != null)
                    result += $"{columns[i]} = '{values[i]}'" + (i + 1 != columns.Length ? ", " : " ");
                else
                    result += $"{columns[i]} = null" + (i + 1 != columns.Length ? ", " : " ");
            //replacing the markers with the desired column names and values
            return query.Replace(":update:", result);
        }
        public static void AssetCutstring(string name, string value)
        {
            if (name.IndexOf("&nbsp;") > -1)
            {
                if (Regex.Matches(name, "&nbsp;").Count == 3)
                {
                    name = name.Replace("&nbsp;", "");
                    sub = name;
                    if (name == "ในประเทศ - มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        asset.Domestic_interrest = value;
                    else if (name == "ในประเทศ - ไม่มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        asset.Domestic_non_interest = value;
                    else if (name == "ต่างประเทศ - มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        asset.Foreign_interest_bearing = value;
                    else if (name == "ต่างประเทศ - ไม่มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        asset.Foreign_non_interest_bearing = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "ลูกหนี้การค้าและลูกหนี้อื่น")
                        asset.Other_parties = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "ลูกหนี้การค้าและลูกหนี้อื่น")
                        asset.Related_parties_receivable = value;
                    else if (name == "หัก ค่าเผื่อหนี้สงสัยจะสูญ" && main == "ลูกหนี้การค้าและลูกหนี้อื่น")
                        asset.Less_allowance_doubtful = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "ลูกหนี้ระยะยาวอื่นที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Other_parties_long_receivables = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "ลูกหนี้ระยะยาวอื่นที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Related_parties_long_receivable = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "มูลค่างานที่เสร็จยังไม่ได้เรียกเก็บ")
                        asset.Other_parties_unbilled = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "เงินให้กู้ยืมระยะยาวที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Related_parties_long = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เงินให้กู้ยืมระยะยาวที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Other_parties_long_current = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เงินทดรองจ่ายและเงินให้กู้ยืมระยะสั้น")
                        asset.Other_parties_short = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "เงินทดรองจ่ายและเงินให้กู้ยืมระยะสั้น")
                        asset.Related_parties = value;
                    else if (name == "ต้นทุนโครงการพัฒนาอสังหาริมทรัพย์" && main == "สินค้าคงเหลือ")
                        asset.Real_estate_costs_Inventories = value;
                    else if (name == "สินค้าสำเร็จรูป" && main == "สินค้าคงเหลือ")
                        asset.Finished_goods = value;
                    else if (name == "สินค้าระหว่างทาง" && main == "สินค้าคงเหลือ")
                        asset.Goods_in_transit = value;
                    else if (name == "อสังหาริมทรัพย์รอการขาย" && main == "สินค้าคงเหลือ")
                        asset.Estate_for_sales = value;
                    else if (name == "งานระหว่างทำ" && main == "สินค้าคงเหลือ")
                        asset.Work_in_progress = value;
                    else if (name == "วัตถุดิบ อะไหล่และวัสดุสิ้นเปลือง" && main == "สินค้าคงเหลือ")
                        asset.Raw_material = value;
                    else if (name == "หัก ค่าเผื่อการลดมูลค่าของสินค้าคงเหลือ" && main == "สินค้าคงเหลือ")
                        asset.Less_allowance_diminution = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "ลูกหนี้ระยะสั้นอื่น - สุทธิ")
                        asset.Other_parties_short_receivable = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "ลูกหนี้ระยะสั้นอื่น - สุทธิ")
                        asset.Related_parties_short = value;
                    else if (name == "เงินปันผลค้างรับ" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Dividend_receivables = value;
                    else if (name == "เงินมัดจำ" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Refundable_deposits_current = value;
                    else if (name == "เงินประกันผลงาน" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Amount_retention_construction = value;
                    else if (name == "เงินจ่ายล่วงหน้า" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Avance_payments_current = value;
                    else if (name == "ภาษีเงินได้หัก ณ ที่จ่าย" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Withholding_tax = value;
                    else if (name == "ค่าใช้จ่ายจ่ายล่วงหน้า" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Prepayments_current = value;
                    else if (name == "รายได้ค้างรับ" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Accrued_income = value;
                    else if (name == "ดอกเบี้ยค้างรับ" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Accrued_interest_current = value;
                    else if (name == "ภาษีมูลค่าเพิ่มรอเรียกคืน" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Refundable_value_added_tax = value;
                    else if (name == "ภาษีมูลค่าเพิ่มค้างรับ" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Receivables_value_added_tax = value;
                    else if (name == "ภาษีซื้อที่ยังไม่ถึงกำหนด" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Value_added_tax = value;
                    else if (name == "สินทรัพย์หมุนเวียนอื่น - อื่น ๆ" && main == "สินทรัพย์หมุนเวียนอื่น")
                        asset.Other_current_assets_others = value;
                    else if (name == "ตราสารหนี้ที่จะถือจนครบกำหนด" && main == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Debt_securities = value;
                    else if (name == "เงินลงทุนเผื่อขาย" && main == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Affiliates_investment = value;
                    else if (name == "เงินลงทุนระยะยาว" && main == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Long_investments = value;
                    else if (name == "หัก ค่าเผื่อการด้อยค่าของเงินลงทุน" && main == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Less_allowance_investments = value;
                    else if (name == "อสังหาริมทรัพย์เพื่อการลงทุน" && main == "อสังหาริมทรัพย์เพื่อการลงทุน - สุทธิ")
                        asset.Investment_properties = value;
                    else if (name == "หัก ค่าเสื่อมราคาสะสมอสังหาริทรัพย์เพื่อการลงทุน" && main == "อสังหาริมทรัพย์เพื่อการลงทุน - สุทธิ")
                        asset.Less_accumulated_depreciation = value;
                    else if (name == "หัก ค่าเผื่อการด้อยค่าอสังหาริมทรัพย์เพื่อการลงทุน" && main == "อสังหาริมทรัพย์เพื่อการลงทุน - สุทธิ")
                        asset.Less_allowance_impairment = value;
                    else if (name == "ค่าความนิยม" && main == "ค่าความนิยม - สุทธิ")
                        asset.Goodwill = value;
                    else if (name == "เงินลงทุนที่จะถือจนครบกำหนด" && main == "เงินลงทุน - สุทธิ")
                        asset.To_maturity_investments = value;
                    else if (name == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน" && main == "เงินลงทุน - สุทธิ")
                        asset.Investment_account_net = value;
                    else if (name == "หัก ค่าเผื่อการด้อยค่าของเงินลงทุน" && main == "เงินลงทุน - สุทธิ")
                        asset.Allowance_impairment_investments = value;
                    else if (name == "เงินลงทุนเผื่อขาย" && main == "เงินลงทุน - สุทธิ")
                        asset.Available_sale_investments = value;
                    else if (name == "เงินลงทุนเพื่อค้า" && main == "เงินลงทุน - สุทธิ")
                        asset.Investment_trading_securities_net = value;
                    else if (name == "เงินลงทุนอื่น" && main == "เงินลงทุน - สุทธิ")
                        asset.Other_investment = value;
                    else if (name == "เงินลงทุนในบริษัทร่วม กิจการร่วมค้า และ/หรือ กิจการที่ควบคุมร่วมกันซึ่งบันทึกโดยวิธีส่วนได้เสีย" && main == "เงินลงทุน - สุทธิ")
                        asset.Investment_joint_ventures_net = value;
                    else if (name == "เงินให้กู้ยืมและลูกหนี้" && main == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                        asset.Loans_and_receivables = value;
                    else if (name == "ดอกเบี้ยค้างรับ" && main == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                        asset.Accrued_interest_receivables = value;
                    else if (name == "หัก รายได้รอตัดบัญชี" && main == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                        asset.Deferred_revenue = value;
                    else if (name == "หัก ค่าเผื่อหนี้สงสัยจะสูญ" && main == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                        asset.Less_doubtful_accounts = value;
                    else if (name == "หัก ค่าเผื่อการปรับมูลค่าจากการปรับโครงสร้างหนี้" && main == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                        asset.Less_revaluation_allowance = value;
                    else if (name == "ลูกหนี้ธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า" && main == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                        asset.Securities_derivatives_receivables = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "เงินให้กู้ยืมระยะยาว - สุทธิจากส่วนที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Related_parties_long_loans = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เงินให้กู้ยืมระยะยาว - สุทธิจากส่วนที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Other_parties_long = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "ลูกหนี้ระยะยาวอื่น - สุทธิจากส่วนที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Other_parties_portion = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "ลูกหนี้ระยะยาวอื่น - สุทธิจากส่วนที่ถึงกำหนดภายในหนึ่งปี")
                        asset.Related_parties_portion = value;
                    else if (name == "สินทรัพย์ชีวภาพ" && main == "สินทรัพย์ชีวภาพ - สุทธิ")
                        asset.Biological_assets = value;
                    else if (name == "ที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                        asset.Property = value;
                    else if (name == "ที่ดิน อาคาร และอุปกรณ์ตามสัญญาเช่าการเงิน" && main == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                        asset.Property_finance_leases = value;
                    else if (name == "ส่วนปรับปรุงที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                        asset.Improvement_property = value;
                    else if (name == "หัก ค่าเสื่อมราคาสะสมที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                        asset.Less_depreciation_property = value;
                    else if (name == "หัก ค่าเผื่อการด้อยค่าที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                        asset.Less_allowance_property = value;
                    else if (name == "ส่วนที่เพิ่มขึ้นจากการตีราคาที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                        asset.Revaluation_property = value;
                    else if (name == "สิทธิในสัมปทาน" && main == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                        asset.Concession_rights = value;
                    else if (name == "สินทรัพย์ไม่มีตัวตนอื่น" && main == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                        asset.Other_intangible_assets = value;
                    else if (name == "ค่าลิขสิทธิ์ซอฟแวร์" && main == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                        asset.Software_licances = value;
                    else if (name == "เครื่องหมายการค้า" && main == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                        asset.Trade_mark = value;
                    else if (name == "หัก ค่าตัดจำหน่ายสะสมสินทรัพย์ไม่มีตัวตน" && main == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                        asset.Less_accumulated = value;
                    else if (name == "หัก ค่าเผื่อการด้อยค่าสินทรัพย์ไม่มีตัวตน" && main == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                        asset.Less_impairment = value;
                    else if (name == "สินทรัพย์ตราสารอนุพันธ์" && main == "สินทรัพย์ทางการเงิน")
                        asset.Derivative_assets = value;
                    else if (name == "สินทรัพย์ทางการเงินอื่น" && main == "สินทรัพย์ทางการเงิน")
                        asset.Other_financial = value;
                    else if (name == "ค่าใช้จ่ายรอตัดบัญชี" && main == "สินทรัพย์ไม่หมุนเวียนอื่น")
                        asset.Deferred_expenditure = value;
                    else if (name == "เงินจ่ายล่วงหน้า" && main == "สินทรัพย์ไม่หมุนเวียนอื่น")
                        asset.Avance_payments = value;
                    else if (name == "ค่าใช้จ่ายจ่ายล่วงหน้า" && main == "สินทรัพย์ไม่หมุนเวียนอื่น")
                        asset.Prepaid_expenses = value;
                    else if (name == "เงินมัดจำ" && main == "สินทรัพย์ไม่หมุนเวียนอื่น")
                        asset.Refundable_deposits_non = value;
                    else if (name == "ภาษีเงินได้ระหว่างการขอคืน" && main == "สินทรัพย์ไม่หมุนเวียนอื่น")
                        asset.Refundable_income = value;
                    else if (name == "สินทรัพย์ไม่หมุนเวียน - อื่น ๆ" && main == "สินทรัพย์ไม่หมุนเวียนอื่น")
                        asset.Non_current_assets_other = value;
                    else if (name == "เงินจ่ายล่วงหน้า" && main == "สินทรัพย์อื่น - สุทธิ")
                        asset.Advence_payments = value;
                    else if (name == "ค่าใช้จ่ายจ่ายล่วงหน้า" && main == "สินทรัพย์อื่น - สุทธิ")
                        asset.Prepayments = value;
                    else if (name == "เงินมัดจำ" && main == "สินทรัพย์อื่น - สุทธิ")
                        asset.Refundable_deposits = value;
                    else if (name == "สินทรัพย์อื่น - อื่น ๆ" && main == "สินทรัพย์อื่น - สุทธิ")
                        asset.Other_assets_other = value;
                    else if (name == "ค่าใช้จ่ายรอตัดบัญชี" && main == "สินทรัพย์อื่น - สุทธิ")
                        asset.Deferred_expenditure_assets = value;
                    else if (name == "บริษัทร่วม กิจการร่วมค้า และ/หรือ กิจการที่ควบคุมร่วมกัน" && main == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Associates = value;
                    else if (name == "กิจการที่เกี่ยวข้องกัน" && main == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Affiliates_investment = value;
                    else
                        log.LOGW($"[FundamentalSET100::AssetCutstring] [{main}]: {name} {value}");
                }
                else if (Regex.Matches(name, "&nbsp;").Count == 6)
                {
                    name = name.Replace("&nbsp;", "");
                    if (name == "กิจการที่เกี่ยวข้องกัน" && main == "เงินลงทุน - สุทธิ" && sub == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Affiliates = value;
                    else if (name == "บริษัทร่วม กิจการร่วมค้า และ/หรือ กิจการที่ควบคุมร่วมกัน" && main == "เงินลงทุน - สุทธิ" && sub == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                        asset.Associates_net = value;
                    else
                        log.LOGW($"[FundamentalSET100::AssetCutstring] [{main}] [{sub}]: {name} {value}");
                }
                else
                    log.LOGW($"[FundamentalSET100::AssetCutstring] [{main}]: {name} {value}");
            }
            else
            {
                main = name;
                if (name == "เงินสดและรายการเทียบเท่าเงินสด")
                    asset.Cash_equivalents = value;
                else if (name == "รายการระหว่างธนาคารและตลาดเงิน")
                    asset.Interbank_money_market = value;
                else if (name == "เงินให้สินเชื่อแก่สถาบันการเงิน")
                    asset.Loans_financial_institutions = value;
                else if (name == "เงินลงทุนระยะสั้นที่มีภาระผูกพัน")
                    asset.Short_Investments_restricted = value;
                else if (name == "เงินลงทุนระยะสั้น")
                    asset.Short_Investments = value;
                else if (name == "เงินลงทุนเพื่อค้า")
                    asset.Investment_trading_securities = value;
                else if (name == "ลูกหนี้การค้าและลูกหนี้อื่น")
                    asset.Other_receivable = value;
                else if (name == "ลูกหนี้ระยะยาวอื่นที่ถึงกำหนดภายในหนึ่งปี")
                    asset.Current_portion_long_receivables = value;
                else if (name == "มูลค่างานที่เสร็จยังไม่ได้เรียกเก็บ")
                    asset.Unbilled_receivables = value;
                else if (name == "เงินให้กู้ยืมระยะยาวที่ถึงกำหนดภายในหนึ่งปี")
                    asset.Current_portion_long = value;
                else if (name == "เงินทดรองจ่ายและเงินให้กู้ยืมระยะสั้น")
                    asset.Short_loans = value;
                else if (name == "สินค้าคงเหลือ")
                    asset.Inventories = value;
                else if (name == "สินทรัพย์ไม่หมุนเวียน และ/หรือ กลุ่มสินทรัพย์ที่ยกเลิกซึ่งถือไว้เพื่อขาย")
                    asset.Non_current_for_sale = value;
                else if (name == "สินทรัพย์ทางการเงินหมุนเวียนอื่น")
                    asset.Other_current_financial = value;
                else if (name == "สินทรัพย์ตราสารอนุพันธ์หมุนเวียน")
                    asset.Derivative_assets_cerrent = value;
                else if (name == "ลูกหนี้ระยะสั้นอื่น - สุทธิ")
                    asset.Other_short_receivable = value;
                else if (name == "สินทรัพย์หมุนเวียนอื่น")
                    asset.Other_current_assets = value;
                else if (name == "รวมสินทรัพย์หมุนเวียน")
                    asset.Total_current_assets = value;
                else if (name == "เงินลงทุนในบริษัทร่วม กิจการร่วมค้า และ/หรือ กิจการที่ควบคุมร่วมกันซึ่งบันทึกโดยวิธีส่วนได้เสีย")
                    asset.Investment_joint_ventures = value;
                else if (name == "เงินฝากสถาบันการเงินที่มีข้อจำกัดในการใช้")
                    asset.Cash_restricted = value;
                else if (name == "เงินลงทุนซึ่งบันทึกโดยวิธีราคาทุน")
                    asset.Investment_account = value;
                else if (name == "อสังหาริมทรัพย์เพื่อการลงทุน - สุทธิ")
                    asset.Investment_properties_net = value;
                else if (name == "ต้นทุนโครงการพัฒนาอสังหาริมทรัพย์")
                    asset.Real_estate_costs = value;
                else if (name == "เงินให้กู้ยืมระยะยาว - สุทธิจากส่วนที่ถึงกำหนดภายในหนึ่งปี")
                    asset.Long_loans = value;
                else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน")
                    asset.Assets_agreements = value;
                else if (name == "ค่าความนิยม - สุทธิ")
                    asset.Goodwill_net = value;
                else if (name == "เงินลงทุน - สุทธิ")
                    asset.Investment_net = value;
                else if (name == "ลูกหนี้สำนักหักบัญชี")
                    asset.Receivables_clearing_house = value;
                else if (name == "เงินให้สินเชื่อ ลูกหนี้ และดอกเบี้ยค้างรับ - สุทธิ")
                    asset.Loans_receivable_net = value;
                else if (name == "ภาระของลูกค้าจากการรับรอง")
                    asset.Customers_liabilities = value;
                else if (name == "สิทธิในการเรียกคืนหลักทรัพย์ / ภาระของลูกค้าจากการประกอบธุรกิจอื่น")
                    asset.Claim_security = value;
                else if (name == "ทรัพย์สินรอการขาย - สุทธิ")
                    asset.Assets_forclosed = value;
                else if (name == "ลูกหนี้ระยะยาวอื่น - สุทธิจากส่วนที่ถึงกำหนดภายในหนึ่งปี")
                    asset.Net_current_portion = value;
                else if (name == "สินทรัพย์ชีวภาพ - สุทธิ")
                    asset.Biological_assets_net = value;
                else if (name == "ที่ดิน อาคาร และอุปกรณ์ - สุทธิ")
                    asset.Property_net = value;
                else if (name == "สิทธิการเช่า - สุทธิ")
                    asset.Leasehold_right = value;
                else if (name == "สินทรัพย์ภายใต้สัญญาสัมปทาน")
                    asset.Assets_under_concession = value;
                else if (name == "สินทรัพย์ไม่มีตัวตน - สุทธิ")
                    asset.Intangible_assets = value;
                else if (name == "สินทรัพย์ตราสารอนุพันธ์ไม่หมุนเวียน")
                    asset.Non_derivative_assets = value;
                else if (name == "สินทรัพย์ทางการเงิน")
                    asset.Financial_assets = value;
                else if (name == "สินทรัพย์ทางการเงินไม่หมุนเวียนอื่น")
                    asset.Other_non_current_financial = value;
                else if (name == "สินทรัพย์ภาษีเงินได้รอตัดบัญชี")
                    asset.Defferred_tax_assets = value;
                else if (name == "ลูกหนี้ค้างรับ - สุทธิ")
                    asset.Account_receivables = value;
                else if (name == "สินทรัพย์ไม่หมุนเวียนอื่น")
                    asset.Non_current_assets = value;
                else if (name == "ลูกหนี้อื่น - สุทธิ")
                    asset.Other_receivables_net = value;
                else if (name == "สินทรัพย์อื่น - สุทธิ")
                    asset.Other_assets_net = value;
                else if (name == "รวมสินทรัพย์ไม่หมุนเวียน")
                    asset.Total_non_current_assets = value;
                else if (name == "รวมสินทรัพย์")
                    asset.Total_assets = value;
                else if (name == "ลูกหนี้จากการขายเงินลงทุน")
                    asset.Receivables_selling_investments = value;
                else
                    log.LOGW($"[FundamentalSET100::AssetCutstring] {name} {value}");
            }
        }
        public static void DebtCutstring(string name, string value)
        {
            if (name.IndexOf("&nbsp;") > -1)
            {
                if (Regex.Matches(name, "&nbsp;").Count == 3)
                {
                    name = name.Replace("&nbsp;", "");
                    sub = name;
                    if (name == "ภาษีเงินได้นิติบุคคลค้างจ่าย" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Tax_payables = value;
                    else if (name == "หนี้สินหมุนเวียนอื่น - อื่น ๆ" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Current_liabilities_others = value;
                    else if (name == "ค่าใช้จ่ายค้างจ่าย" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Accrued_expense = value;
                    else if (name == "ค่าตอบแทนค้างจ่ายผู้ให้สัมปทานสิทธิ" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Accrued_concession_fees = value;
                    else if (name == "เงินประกันผลงาน" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Amount_retention = value;
                    else if (name == "ดอกเบี้ยค้างจ่าย" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Accrued_interest_payable = value;
                    else if (name == "ภาษีสรรพสามิตค้างจ่าย" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Excise_duty_payable = value;
                    else if (name == "ภาษีมูลค่าเพิ่มค้างจ่าย" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Added_tax_payable = value;
                    else if (name == "เงินมัดจำ" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Refundable_deposits_liabilities = value;
                    else if (name == "เงินปันผลค้างจ่าย" && main == "หนี้สินหมุนเวียนอื่น")
                        debt.Dividend_payable = value;
                    else if (name == "เงินมัดจำ" && main == "หนี้สินไม่หมุนเวียนอื่น")
                        debt.Refundable_deposits = value;
                    else if (name == "หนี้สินไม่หมุนเวียน - อื่น ๆ" && main == "หนี้สินไม่หมุนเวียนอื่น")
                        debt.Non_current_liabilities_other = value;
                    else if (name == "เงินประกันผลงาน" && main == "หนี้สินไม่หมุนเวียนอื่น")
                        debt.Retention = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เจ้าหนี้ระยะสั้นอื่น - สุทธิ")
                        debt.Other_partties_short = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "เจ้าหนี้ระยะสั้นอื่น - สุทธิ")
                        debt.Related_parties_short = value;
                    else if (name == "เงินกู้ยืมระยะยาว - สถาบันการเงิน" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Long_borrowings = value;
                    else if (name == "หนี้สินตามสัญญาเช่าการเงิน" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Finance_lease_liabilities = value;
                    else if (name == "หนี้สินตามสัมปทานสิทธิ" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Liabilities_concession_long = value;
                    else if (name == "หนี้สินระยะยาวอื่น" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Liabilities_under_concession = value;
                    else if (name == "ตราสารหนี้" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Debt_certificates_long = value;
                    else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Long_borrowings_related = value;
                    else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการอื่น" && main == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Long_borrowings_other_parties = value;
                    else if (name == "หนี้สินตามสัญญาเช่าการเงิน" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Finance_lease_liabilities_net = value;
                    else if (name == "ตราสารหนี้" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Debt_certificates = value;
                    else if (name == "หนี้สินตามสัมปทานสิทธิ" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Liabilities_concession_long_net = value;
                    else if (name == "หนี้สินระยะยาวอื่น" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Liabilities_under_concession_net = value;
                    else if (name == "เงินกู้ยืมระยะยาว - สถาบันการเงิน" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Long_borrowings_financial = value;
                    else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Current_portion_received_net = value;
                    else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการอื่น" && main == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Long_borrowings_parties = value;
                    else if (name == "เงินกู้ยืม" && main == "เงินกู้ยืมและเงินรับฝาก")
                        debt.Borrowings = value;
                    else if (name == "เงินรับฝาก" && main == "เงินกู้ยืมและเงินรับฝาก")
                        debt.Deposits = value;
                    else if (name == "หนี้สินทางการเงินอื่น" && main == "หนี้สินทางการเงิน")
                        debt.Other_financial_liabilities = value;
                    else if (name == "หนี้สินตราสารอนุพันธ์" && main == "หนี้สินทางการเงิน")
                        debt.Derivative_liabilities_financial = value;
                    else if (name == "หนี้สินทางการเงินที่กำหนดให้แสดงด้วยมูลค่ายุติธรรม" && main == "หนี้สินทางการเงิน")
                        debt.Financial_fair_value = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เจ้าหนี้การค้าและเจ้าหนี้อื่น")
                        debt.Other_partties_trade = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "เจ้าหนี้การค้าและเจ้าหนี้อื่น")
                        debt.Related_parties_trade = value;
                    else if (name == "บุคคลหรือกิจการที่เกี่ยวข้องกัน" && main == "เงินทดรองรับและเงินกู้ยืมระยะสั้น")
                        debt.Related_parties_short_loans = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เงินทดรองรับและเงินกู้ยืมระยะสั้น")
                        debt.Other_partties_short_loans = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เจ้าหนี้ระยะยาวอื่น - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Other_partties_portion = value;
                    else if (name == "บุคคลหรือกิจการอื่น" && main == "เจ้าหนี้ระยะยาวอื่นที่ถึงกำหนดชำระภายในหนึ่งปี")
                        debt.Other_parties_account = value;
                    else if (name == "ในประเทศ - มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        debt.Domestic_interest_bearing = value;
                    else if (name == "ในประเทศ - ไม่มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        debt.Domestic_non_interest_bearing = value;
                    else if (name == "ต่างประเทศ - มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        debt.Foreign_interest_bearing = value;
                    else if (name == "ต่างประเทศ - ไม่มีดอกเบี้ย" && main == "รายการระหว่างธนาคารและตลาดเงิน")
                        debt.Foreign_non_interest_bearing = value;
                    else
                        log.LOGW($"[FundamentalSET100::DebtCutstring] [{main}]: {name} {value}");
                }
                else if (Regex.Matches(name, "&nbsp;").Count == 6)
                {
                    name = name.Replace("&nbsp;", "");

                    if (name == "เงินกู้ยืมระยะสั้น" && main == "เงินกู้ยืมและเงินรับฝาก" && sub == "เงินกู้ยืม")
                        debt.Short_borrowing = value;
                    else if (name == "เงินกู้ยืมระยะยาว" && main == "เงินกู้ยืมและเงินรับฝาก" && sub == "เงินกู้ยืม")
                        debt.Long_borrowing = value;
                    else if (name == "เงินฝากที่เป็นเงินบาท" && main == "เงินกู้ยืมและเงินรับฝาก" && sub == "เงินรับฝาก")
                        debt.Deposits_bath = value;
                    else if (name == "เงินฝากที่เป็นเงินตราต่างประเทศ" && main == "เงินกู้ยืมและเงินรับฝาก" && sub == "เงินรับฝาก")
                        debt.Deposits_foreign_currencies = value;
                    else
                        log.LOGW($"[FundamentalSET100::DebtCutstring] [{main}] [{sub}]: {name} {value}");
                }
                else
                    log.LOGW($"[FundamentalSET100::DebtCutstring] [{main}]: {name} {value}");
            }
            else
            {
                main = name;
                if (name == "เงินเบิกเกินบัญชีและเงินกู้ยืมระยะสั้นจากสถาบันการเงิน")
                    debt.Bank_overdrafts = value;
                else if (name == "เจ้าหนี้การค้าและเจ้าหนี้อื่น")
                    debt.Trade_other_payable = value;
                else if (name == "เจ้าหนี้ระยะสั้นอื่น - สุทธิ")
                    debt.Short_account_payables = value;
                else if (name == "ส่วนของหนี้สินระยะยาวที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Long_liabilities = value;
                else if (name == "หนี้สินตราสารอนุพันธ์หมุนเวียน")
                    debt.Derivative_liabilities = value;
                else if (name == "ประมาณการหนี้สินระยะสั้น")
                    debt.Short_provisions = value;
                else if (name == "หนี้สินหมุนเวียนอื่น")
                    debt.Current_liabilities = value;
                else if (name == "รวมหนี้สินหมุนเวียน")
                    debt.Total_current_liabilities = value;
                else if (name == "หนี้สินระยะยาว - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Net_long_liabilities = value;
                else if (name == "หนี้สินตราสารอนุพันธ์ไม่หมุนเวียน")
                    debt.Non_derivative_liabilities = value;
                else if (name == "ประมาณการหนี้สินระยะยาว")
                    debt.Long_provisions = value;
                else if (name == "หนี้สินผลประโยชน์พนักงานภายหลังเลิกจ้าง - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Post_employee_obligtions = value;
                else if (name == "หนี้สินภาษีเงินได้รอตัดบัญชี")
                    debt.Defferred_tax_liabilities = value;
                else if (name == "หนี้สินไม่หมุนเวียนอื่น")
                    debt.Non_current_liabilities = value;
                else if (name == "รวมหนี้สินไม่หมุนเวียน")
                    debt.Total_non_current_liabilities = value;
                else if (name == "รวมหนี้สิน")
                    debt.Total_liabilities = value;
                else if (name == "รายได้รอการรับรู้ - ส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Current_portion = value;
                else if (name == "เงินกู้ยืมและเงินรับฝาก")
                    debt.Borrowings_deposits = value;
                else if (name == "ดอกเบี้ยค้างจ่าย")
                    debt.Accrues_interest_payable = value;
                else if (name == "หุ้นกู้และตราสารหนี้อื่น")
                    debt.Debentures_other_debt = value;
                else if (name == "หนี้สินทางการเงิน")
                    debt.Financial_liabilities = value;
                else if (name == "ประมาณการหนี้สิน")
                    debt.Provisions = value;
                else if (name == "เจ้าหนี้ค้างจ่าย")
                    debt.Account_payables = value;
                else if (name == "เจ้าหนี้อื่น")
                    debt.Other_payable = value;
                else if (name == "ภาษีเงินได้ค้างจ่าย")
                    debt.Income_tax_payable = value;
                else if (name == "หนี้สินอื่น")
                    debt.Other_liabilities = value;
                else if (name == "เงินรับล่วงหน้าจากลูกค้า - ส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Current_portion_received = value;
                else if (name == "เงินทดรองรับและเงินกู้ยืมระยะสั้น")
                    debt.Advances_short_loans = value;
                else if (name == "รายได้รอการรับรู้ - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Net_deferred_income = value;
                else if (name == "เงินรับล่วงหน้าจากลูกค้า - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Net_received_customers = value;
                else if (name == "เจ้าหนี้ระยะยาวอื่น - สุทธิจากส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Net_current_portion = value;
                else if (name == "รายการระหว่างธนาคารและตลาดเงิน")
                    debt.Interbank_money_market = value;
                else if (name == "หนี้สินจ่ายคืนเมื่อทวงถาม")
                    debt.Liabilities_payable_demand = value;
                else if (name == "ภาระจากการรับรอง")
                    debt.Liabilities_under_acceptances = value;
                else if (name == "เจ้าหนี้ค่าที่ดินและค่าก่อสร้าง")
                    debt.Land_cost_payable = value;
                else if (name == "หนี้สินผลประโยชน์พนักงานภายหลังเลิกจ้าง - ส่วนที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Current_benefit_obligations = value;
                else if (name == "หนี้สินที่เกี่ยวข้องโดยตรงกับสินทรัพย์ไม่หมุนเวียน และ/หรือ กลุ่มสินทรัพย์ที่ยกเลิกซึ่งถือไว้เพื่อขาย")
                    debt.Liabilities_non_current = value;
                else if (name == "เจ้าหนี้สำนักหักบัญชี")
                    debt.Payable_clearing_house = value;
                else if (name == "เจ้าหนี้ธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า")
                    debt.Securities_business_payables = value;
                else if (name == "ภาระในการส่งคืนหลักทรัพย์")
                    debt.Claims_on_security = value;
                else if (name == "เจ้าหนี้ทรัสต์รีซีท")
                    debt.Liabilities_turst_receipts = value;
                else if (name == "เจ้าหนี้จากการซื้อเงินลงทุน")
                    debt.Payables_purchases_securities = value;
                else if (name == "ส่วนแบ่งผลขาดทุนที่เกินเงินลงทุนในบริษัทร่วม และ/หรือ กิจการร่วมค้า")
                    debt.Excess_loss_cost = value;
                else if (name == "เจ้าหนี้ระยะยาวอื่นที่ถึงกำหนดชำระภายในหนึ่งปี")
                    debt.Current_portion_account = value;
                else
                    log.LOGW($"[FundamentalSET100::DebtCutstring] {name} {value}");
            }
        }
        public static void ShareHolderEquityCutstring(string name, string value)
        {
            if (name.IndexOf("&nbsp;") > -1)
            {
                if (Regex.Matches(name, "&nbsp;").Count == 3)
                {
                    name = name.Replace("&nbsp;", "");
                    sub = name;
                    if (name == "หุ้นสามัญ" && main == "ทุนจดทะเบียน")
                        share_holder_equity.Share_ordinary_shares = value;
                    else if (name == "หุ้นบุริมสิทธิ" && main == "ทุนจดทะเบียน")
                        share_holder_equity.Preferred_shares = value;
                    else if (name == "หุ้นสามัญ" && main == "ทุนที่ออกและชำระเต็มมูลค่าแล้ว")
                        share_holder_equity.Fully_ordinary_shares = value;
                    else if (name == "หุ้นบุริมสิทธิ" && main == "ทุนที่ออกและชำระเต็มมูลค่าแล้ว")
                        share_holder_equity.Preferred_shares_fully = value;
                    else if (name == "หุ้นสามัญ" && main == "ส่วนเกิน (ต่ำกว่า) มูลค่าหุ้น")
                        share_holder_equity.Premium_ordinary_shares = value;
                    else if (name == "หุ้นบุริมสิทธิ" && main == "ส่วนเกิน (ต่ำกว่า) มูลค่าหุ้น")
                        share_holder_equity.Preferred_shares_premium = value;
                    else if (name == "กำไร (ขาดทุน) สะสม - ยังไม่ได้จัดสรร" && main == "กำไร (ขาดทุน) สะสม")
                        share_holder_equity.Retained_earnings_unappropriated = value;
                    else if (name == "กำไรสะสม - จัดสรรแล้ว" && main == "กำไร (ขาดทุน) สะสม")
                        share_holder_equity.Retained_earnings_appropriated = value;
                    else if (name == "เงินรับล่วงหน้าค่าหุ้น" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น")
                        share_holder_equity.Share_subscription_received = value;
                    else if (name == "รายการอื่น" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น")
                        share_holder_equity.Other_items = value;
                    else if (name == "ผลต่างจากการแปลงค่างบการเงิน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น")
                        share_holder_equity.Currency_translation_changes = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น")
                        share_holder_equity.Other_surplus = value;
                    else if (name == "จำนวนหุ้นทุนซื้อคืน (หน่วย : หุ้น)" && main == "หุ้นทุนรับซื้อคืน / หุ้นของบริษัทที่ถือโดยบริษัทย่อย")
                        share_holder_equity.Number_treasury_shares = value;
                    else if (name == "หุ้นทุนซื้อคืน" && main == "หุ้นทุนรับซื้อคืน / หุ้นของบริษัทที่ถือโดยบริษัทย่อย")
                        share_holder_equity.Treasury_shares = value;
                    else if (name == "หุ้นของบริษัทที่ถือโดยบริษัทย่อย" && main == "หุ้นทุนรับซื้อคืน / หุ้นของบริษัทที่ถือโดยบริษัทย่อย")
                        share_holder_equity.Shares_subsidiaries = value;
                    else
                        log.LOGW($"[FundamentalSET100::ShareHolderEquityCutstring] [{main}]: {name} {value}");
                }
                else if (Regex.Matches(name, "&nbsp;").Count == 6)
                {
                    name = name.Replace("&nbsp;", "");
                    nano = name;
                    if (name == "สำรองตามกฎหมาย" && main == "กำไร (ขาดทุน) สะสม" && sub == "กำไรสะสม - จัดสรรแล้ว")
                        share_holder_equity.Legal_statutory_reserves = value;
                    else if (name == "สำรองเพื่อประกันตนเอง" && main == "กำไร (ขาดทุน) สะสม" && sub == "กำไรสะสม - จัดสรรแล้ว")
                        share_holder_equity.Self_insurance_reserve = value;
                    else if (name == "สำรองเพื่อขยายงาน" && main == "กำไร (ขาดทุน) สะสม" && sub == "กำไรสะสม - จัดสรรแล้ว")
                        share_holder_equity.Expansion_reserve = value;
                    else if (name == "สำรองอื่น ๆ" && main == "กำไร (ขาดทุน) สะสม" && sub == "กำไรสะสม - จัดสรรแล้ว")
                        share_holder_equity.Other_reserves = value;
                    else if (name == "สำรองเพื่อซื้อหุ้นทุนซื้อคืน" && main == "กำไร (ขาดทุน) สะสม" && sub == "กำไรสะสม - จัดสรรแล้ว")
                        share_holder_equity.Treasury_shares_reserve = value;
                    else if (name == "สำรองเพื่อชำระคืนเงินกู้ยืม" && main == "กำไร (ขาดทุน) สะสม" && sub == "กำไรสะสม - จัดสรรแล้ว")
                        share_holder_equity.Loan_repayment_reserve = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนจากการเปลี่ยนแปลงมูลค่ายุติธรรมของเงินลงทุน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Revaluation_surplus = value;
                    else if (name == "กำไร (ขาดทุน) ที่ยังไม่เกิดขึ้นจากการเปลี่ยนแปลงสัดส่วนการลงทุน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Unrealised_gain_investments = value;
                    else if (name == "กำไร (ขาดทุน) จากการทำสัญญาป้องกันความเสี่ยง" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Gains_instruments = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนจากรายการในส่วนของผู้ถือหุ้นของบริษัทย่อย และ/หรือ บริษัทร่วม" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Surplus_subsidiaries_associates = value;
                    else if (name == "ส่วนเกินทุนจากใบสำคัญแสดงสิทธิที่หมดอายุแล้ว" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Expired_warrants_surplus = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนจากการรวมธุรกิจภายใต้การควบคุมเดียวกัน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Surplus_combinations_control = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น - อื่น ๆ" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Other_surplus_others = value;
                    else if (name == "ส่วนเกินทุนจากการลดทุน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Premium_capital_reduction = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนจากการเพิ่มทุนของบริษัทย่อย และ/หรือ บริษัทร่วม" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Surplus_subsidiaries_associates_increase = value;
                    else if (name == "ส่วนเกินทุนจากการตีราคาสินทรัพย์ถาวร" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Revalution_fixed_assets = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนจากหุ้นทุนซื้อคืน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น")
                        share_holder_equity.Premium_treasury_shares = value;
                    else
                        log.LOGW($"[FundamentalSET100::ShareHolderEquityCutstring] [{main}] [{sub}]: {name} {value}");
                }
                else if (Regex.Matches(name, "&nbsp;").Count == 9)
                {
                    name = name.Replace("&nbsp;", "");
                    if (name == "อื่น ๆ" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น" && nano == "ส่วนเกิน (ต่ำกว่า) ทุนจากรายการในส่วนของผู้ถือหุ้นของบริษัทย่อย และ/หรือ บริษัทร่วม")
                        share_holder_equity.Other_sub_associates = value;
                    else if (name == "ส่วนเกิน (ต่ำกว่า) ทุนจากการเปลี่ยนแปลงมูลค่ายุติธรรมของเงินลงทุน" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น" && nano == "ส่วนเกิน (ต่ำกว่า) ทุนจากรายการในส่วนของผู้ถือหุ้นของบริษัทย่อย และ/หรือ บริษัทร่วม")
                        share_holder_equity.Revaluation_surplus_sub = value;
                    else if (name == "ส่วนเกินทุนจากการตีราคาสินทรัพย์ถาวร" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น" && nano == "ส่วนเกิน (ต่ำกว่า) ทุนจากรายการในส่วนของผู้ถือหุ้นของบริษัทย่อย และ/หรือ บริษัทร่วม")
                        share_holder_equity.Revalution_fixed_assets_sub = value;
                    else if (name == "หุ้นสามัญ" && main == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น" && sub == "ส่วนเกิน (ต่ำกว่า) ทุนอื่น" && nano == "ส่วนเกิน (ต่ำกว่า) ทุนจากหุ้นทุนซื้อคืน")
                        share_holder_equity.Treasury_ordinary_shares = value;
                    else
                        log.LOGW($"[FundamentalSET100::ShareHolderEquityCutstring] [{main}] [{sub}] [{nano}]: {name} {value}");
                }
                else
                    log.LOGW($"[FundamentalSET100::ShareHolderEquityCutstring] [{main}]: {name} {value}");
            }
            else
            {
                main = name;
                if (name == "ทุนจดทะเบียน")
                    share_holder_equity.Share_capital = value;
                else if (name == "ทุนที่ออกและชำระเต็มมูลค่าแล้ว")
                    share_holder_equity.Fully_shares_capital = value;
                else if (name == "ส่วนเกิน (ต่ำกว่า) มูลค่าหุ้น")
                    share_holder_equity.Premium_shares_capital = value;
                else if (name == "กำไร (ขาดทุน) สะสม")
                    share_holder_equity.Retained_earnings = value;
                else if (name == "องค์ประกอบอื่นของส่วนของผู้ถือหุ้น")
                    share_holder_equity.Other_components_equity = value;
                else if (name == "รวมส่วนของผู้ถือหุ้นของบริษัท")
                    share_holder_equity.Equity_attributable = value;
                else if (name == "ส่วนได้เสียที่ไม่มีอำนาจควบคุม")
                    share_holder_equity.Non_current_interests = value;
                else if (name == "รวมส่วนของผู้ถือหุ้น")
                    share_holder_equity.Total_equity = value;
                else if (name == "ใบสำคัญแสดงสิทธิ สิทธิซื้อหุ้น และสิทธิในการเลือกซื้อหุ้น")
                    share_holder_equity.Warrants_options = value;
                else if (name == "หุ้นทุนรับซื้อคืน / หุ้นของบริษัทที่ถือโดยบริษัทย่อย")
                    share_holder_equity.Treasury_shares_subsidiaries = value;
                else
                    log.LOGW($"[FundamentalSET100::ShareHolderEquityCutstring] {name} {value}");
            }
        }
        public static void IncomeCutstring(string name, string value)
        {
            if (name.IndexOf("&nbsp;") > -1)
            {
                if (Regex.Matches(name, "&nbsp;").Count == 3)
                {
                    name = name.Replace("&nbsp;", "");
                    sub = name;
                    if (name == "กำไรจากการปริวรรตเงินตรา" && main == "รายได้อื่น")
                        income.Gain_foreign_exchange = value;
                    else if (name == "รายได้อื่น - อื่น ๆ" && main == "รายได้อื่น")
                        income.Other_income_others = value;
                    else if (name == "ค่าใช้จ่ายในการขาย" && main == "ค่าใช้จ่ายในการขายและบริหาร")
                        income.Selling_expenses = value;
                    else if (name == "ค่าใช้จ่ายในการบริหาร" && main == "ค่าใช้จ่ายในการขายและบริหาร")
                        income.Admin_expenses = value;
                    else if (name == "ค่าใช้จ่ายอื่น - อื่น ๆ" && main == "ค่าใช้จ่ายอื่น")
                        income.Other_expenses_others = value;
                    else if (name == "จากเงินให้สินเชื่อ" && main == "รายได้ดอกเบี้ย")
                        income.Loans = value;
                    else if (name == "จากรายการระหว่างธนาคารและตลาดเงิน" && main == "รายได้ดอกเบี้ย")
                        income.Interbank_money_market = value;
                    else if (name == "จากการให้เช่าซื้อและสัญญาเช่าการเงิน" && main == "รายได้ดอกเบี้ย")
                        income.Finance_lease_income = value;
                    else if (name == "จากเงินลงทุนและธุรกรรมเพื่อค้า" && main == "รายได้ดอกเบี้ย")
                        income.Investment_trading_transactions = value;
                    else if (name == "เงินรับฝาก" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Deposits = value;
                    else if (name == "รายการระหว่างธนาคารและตลาดเงิน" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Interbank_money_market_interest = value;
                    else if (name == "เงินนำส่งกองทุนเพื่อการฟื้นฟูและพัฒนาระบบสถาบันการเงินและสถาบันคุ้มครองเงินฝาก" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Deposits_protection_agency = value;
                    else if (name == "ค่าธรรมเนียมในการกู้ยืมเงิน" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Fee_changes = value;
                    else if (name == "ดอกเบี้ยและส่วนลดจ่าย" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Interest_discounts = value;
                    else if (name == "การรับรอง รับอาวัล และค้ำประกัน" && main == "รายได้ค่าธรรมเนียมและบริการ")
                        income.Acceptances_guarantees = value;
                    else if (name == "อื่น ๆ" && main == "รายได้ค่าธรรมเนียมและบริการ")
                        income.Others_fee_service = value;
                    else if (name == "กำไรจากการปริวรรตเงินตรา" && main == "กำไร (ขาดทุน) สุทธิจากธุรกรรมเพื่อค้าและปริวรรตเงินตรา")
                        income.Gain_on_foreign_exchange = value;
                    else if (name == "กำไรจากการซื้อขายตราสารอนุพันธ์" && main == "กำไร (ขาดทุน) สุทธิจากธุรกรรมเพื่อค้าและปริวรรตเงินตรา")
                        income.Gains_derivative_trading = value;
                    else if (name == "อื่น ๆ" && main == "กำไร (ขาดทุน) สุทธิจากธุรกรรมเพื่อค้าและปริวรรตเงินตรา")
                        income.Other_gain_trading = value;
                    else if (name == "กำไรจากเงินลงทุน" && main == "กำไร (ขาดทุน) สุทธิจากเงินลงทุน")
                        income.Gain_on_investments = value;
                    else if (name == "ส่วนแบ่งกำไรจากเงินลงทุนตามวิธีส่วนได้เสีย" && main == "ส่วนแบ่งกำไร (ขาดทุน) จากเงินลงทุนตามวิธีส่วนได้เสีย")
                        income.Share_profit_investment_account = value;
                    else if (name == "กำไรจากการขายทรัพย์สินรอการขาย" && main == "รายได้จากการดำเนินการอื่น")
                        income.Gain_disposal_properties = value;
                    else if (name == "รายได้อื่น" && main == "รายได้จากการดำเนินการอื่น")
                        income.Other_income_operation = value;
                    else if (name == "ค่าใช้จ่ายเกี่ยวกับพนักงาน" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Personnel_expenses = value;
                    else if (name == "ค่าใช้จ่ายเกี่ยวกับอาคารสถานที่และอุปกรณ์" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Premises_equiment_expenses = value;
                    else if (name == "ค่าภาษีอากร" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Taxes_duties = value;
                    else if (name == "ค่าตอบแทนกรรมการและผู้บริหาร" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Management_remuneration = value;
                    else if (name == "ค่าใช้จ่ายอื่น - อื่น ๆ" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Other_expenses_operation = value;
                    else if (name == "หนี้สูญและหนี้สงสัยจะสูญ" && main == "หนี้สูญ หนี้สงสัยจะสูญ และขาดทุนจากการด้อยค่าของเงินให้สินเชื่อและตราสารหนี้")
                        income.Bad_debt_tax_expenses = value;
                    else if (name == "รายได้จากการขายอสังหาริมทรัพย์" && main == "รายได้จากการขายและหรือการให้บริการ")
                        income.Revenues_sales_real = value;
                    else if (name == "รายได้จากการรับเหมาก่อสร้าง" && main == "รายได้จากการขายและหรือการให้บริการ")
                        income.Revenues_construction_contracts = value;
                    else if (name == "รายได้จากการให้บริการ" && main == "รายได้จากการขายและหรือการให้บริการ")
                        income.Revenues_rendering_service = value;
                    else if (name == "ดอกเบี้ยรับ" && main == "รายได้อื่น")
                        income.Interest_income = value;
                    else if (name == "กำไรจากการขายเงินลงทุน" && main == "รายได้อื่น")
                        income.Gain_disposal_investments = value;
                    else if (name == "ต้นทุนขายอสังหาริมทรัพย์" && main == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                        income.Cost_sales_property = value;
                    else if (name == "ต้นทุนการรับเหมาก่อสร้าง" && main == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                        income.Cost_construction_services = value;
                    else if (name == "ต้นทุนการให้บริการ" && main == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                        income.Cost_rendering_services = value;
                    else if (name == "รายได้จากการขายสินค้า" && main == "รายได้จากการขายและหรือการให้บริการ")
                        income.Revenues_from_sales = value;
                    else if (name == "ต้นทุนขายสินค้า" && main == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                        income.Cost_goods_sold = value;
                    else if (name == "ค่าตอบแทนผู้บริหาร" && main == "ค่าตอบแทนกรรมการและผู้บริหาร")
                        income.Management_remuneration_directors = value;
                    else if (name == "ขาดทุนจากการขายสินทรัพย์ถาวร" && main == "ค่าใช้จ่ายอื่น")
                        income.Loss_disposal_fixed = value;
                    else if (name == "หนี้สูญและหนี้สงสัยจะสูญ" && main == "ค่าใช้จ่ายอื่น")
                        income.Bad_debt_doubtful = value;
                    else if (name == "ขาดทุนจากการด้อยค่าของสินทรัพย์ถาวร" && main == "ค่าใช้จ่ายอื่น")
                        income.Impairment_fixed_assets = value;
                    else if (name == "ขาดทุนจากการด้อยค่าของสินทรัพย์ไม่มีตัวตน" && main == "ค่าใช้จ่ายอื่น")
                        income.Impairment_intangible_assets = value;
                    else if (name == "ขาดทุนจากการลดมูลค่าของสินค้าคงเหลือ" && main == "ค่าใช้จ่ายอื่น")
                        income.Loss_diminution_investories = value;
                    else if (name == "เงินกู้ยืมระยะสั้น" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Short_borrowings = value;
                    else if (name == "เงินกู้ยืมระยะยาว" && main == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                        income.Long_borrowings = value;
                    else if (name == "ขาดทุนจากการขายสินทรัพย์ถาวร" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Loss_disposal_fixed_operating = value;
                    else if (name == "หนี้สูญได้รับคืนและโอนกลับค่าเผื่อหนี้สงสัยจะสูญ" && main == "หนี้สูญ หนี้สงสัยจะสูญ และขาดทุนจากการด้อยค่าของเงินให้สินเชื่อและตราสารหนี้")
                        income.Bad_debt_reversal = value;
                    else if (name == "รายได้ค่านายหน้า" && main == "รายได้จากการดำเนินการอื่น")
                        income.Brokerage_fee = value;
                    else if (name == "กำไรจากการขายทรัพย์สินรอการขาย" && main == "รายได้อื่น")
                        income.Gain_disposal_other_assets = value;
                    else if (name == "กำไรจากการขายสินทรัพย์อื่น" && main == "รายได้อื่น")
                        income.Gain_disposal_other_assets = value;
                    else if (name == "ค่าตอบแทนผู้ให้สัมปทานสิทธิและภาษีสรรพสามิต" && main == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                        income.Concession_fee_excise_tax = value;
                    else if (name == "ขาดทุนจากการปริวรรตเงินตรา" && main == "ค่าใช้จ่ายอื่น")
                        income.Loss_currency_exchange = value;
                    else if (name == "ขาดทุนจากการเปลี่ยนแปลงมูลค่ายุติธรรมของอสังหาริมทรัพย์เพื่อการลงทุน" && main == "ค่าใช้จ่ายอื่น")
                        income.Loss_fair_investment = value;
                    else if (name == "ขาดทุนจากการด้อยค่าของสินทรัพย์อื่น" && main == "ค่าใช้จ่ายอื่น")
                        income.Impairment_loss_other_assets = value;
                    else if (name == "ค่าเสื่อมราคาและค่าตัดจำหน่าย" && main == "ค่าใช้จ่ายอื่น")
                        income.Depreciation_amortisation = value;
                    else if (name == "เงินปันผลรับ" && main == "รายได้อื่น")
                        income.Dividend_income = value;
                    else if (name == "รายได้ค่าเช่าและบริการ" && main == "รายได้อื่น")
                        income.Rental_services_income = value;
                    else if (name == "กำไรจากการขายสินทรัพย์ถาวร" && main == "รายได้อื่น")
                        income.Gain_disposal_investment_income = value;
                    else if (name == "กำไรจากการเปลี่ยนแปลงมูลค่ายุติธรรมของเงินลงทุน" && main == "รายได้อื่น")
                        income.Gain_fair_investments = value;
                    else if (name == "กำไรจากการเปลี่ยนแปลงมูลค่ายุติธรรมของอสังหาริมทรัพย์เพื่อการลงทุน" && main == "รายได้อื่น")
                        income.Gain_fair_properties = value;
                    else if (name == "โอนกลับขาดทุนจากการด้อยค่าของสินทรัพย์อื่น" && main == "รายได้อื่น")
                        income.Reversal_impariment_assets = value;
                    else if (name == "ค่าตอบแทนกรรมการ" && main == "ค่าตอบแทนกรรมการและผู้บริหาร")
                        income.Directors_remuneration_manager = value;
                    else if (name == "ขาดทุนจากการซื้อขายตราสารอนุพันธ์" && main == "ค่าใช้จ่ายอื่น")
                        income.Loss_derivative_trading = value;
                    else if (name == "ค่าใช้จ่ายในการสำรวจ" && main == "ค่าใช้จ่ายอื่น")
                        income.Exploration_expenses = value;
                    else if (name == "ต้นทุนการพัฒนาตัดจำหน่าย" && main == "ค่าใช้จ่ายอื่น")
                        income.Writeoff_development_costs = value;
                    else if (name == "กำไรจากการขายสินทรัพย์อื่น" && main == "รายได้จากการดำเนินการอื่น")
                        income.Gain_disposal_assets = value;
                    else if (name == "โอนกลับขาดทุนจากการด้อยค่าของสินทรัพย์ถาวร" && main == "รายได้อื่น")
                        income.Reversal_loss_fixed_assets = value;
                    else if (name == "หนี้สูญได้รับคืนและโอนกลับค่าเผื่อหนี้สงสัยจะสูญ" && main == "รายได้อื่น")
                        income.Bad_debt_recovery = value;
                    else if (name == "กำไรจากการซื้อขายตราสารอนุพันธ์" && main == "รายได้อื่น")
                        income.Gain_derivative_trading = value;
                    else if (name == "ค่าภาคหลวง" && main == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                        income.Royalty_fee = value;
                    else if (name == "ขาดทุนจากการขายเงินลงทุน" && main == "ค่าใช้จ่ายอื่น")
                        income.Loss_disposal_investments = value;
                    else if (name == "ภาษีธุรกิจเฉพาะและค่าธรรมเนียมโอนอสังหาริมทรัพย์" && main == "ค่าใช้จ่ายอื่น")
                        income.Specific_business_tax = value;
                    else if (name == "จากอื่น ๆ" && main == "รายได้ดอกเบี้ย")
                        income.Other_interest = value;
                    else if (name == "โอนกลับขาดทุนจากการด้อยค่าของทรัพย์สินรอการขาย" && main == "รายได้จากการดำเนินการอื่น")
                        income.Reversal_impairment_forecloses = value;
                    else if (name == "ขาดทุนจากการขายทรัพย์สินรอการขาย" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Loss_disposal_foreclosed = value;
                    else if (name == "ขาดทุนจากการด้อยค่าของทรัพย์สินรอการขาย" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Impairment_loss_foreclosed = value;
                    else if (name == "ค่าใช้จ่ายจากประมาณการหนี้สิน" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                        income.Provision_expenses = value;
                    else if (name == "กำไร (ขาดทุน) จากการดำเนินงานที่ยกเลิก" && main == "รายการอื่น ๆ")
                        income.profit_discontinued_operations = value;
                    else
                        log.LOGW($"[FundamentalSET100::IncomeCutstring] [{main}]: {name} {value}");
                }
                else if (Regex.Matches(name, "&nbsp;").Count == 6)
                {
                    name = name.Replace("&nbsp;", "");
                    nano = name;
                    if (name == "กำไรจากการขายเงินลงทุน" && main == "กำไร (ขาดทุน) สุทธิจากเงินลงทุน" && sub == "กำไรจากเงินลงทุน")
                        income.Gain_disposal_investment = value;
                    else if (name == "โอนกลับขาดทุนจากการด้อยค่าเงินลงทุน" && main == "กำไร (ขาดทุน) สุทธิจากเงินลงทุน" && sub == "กำไรจากเงินลงทุน")
                        income.Reversal_impariment_investment = value;
                    else if (name == "ค่าตอบแทนกรรมการ" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น" && sub == "ค่าตอบแทนกรรมการและผู้บริหาร")
                        income.Directors_remuneration = value;
                    else if (name == "ค่าตอบแทนผู้บริหาร" && main == "ค่าใช้จ่ายในการดำเนินงานอื่น" && sub == "ค่าตอบแทนกรรมการและผู้บริหาร")
                        income.Management_remuneration_operating = value;
                    else
                        log.LOGW($"[FundamentalSET100::IncomeCutstring] [{main}] [{sub}]: {name} {value}");
                }
                else
                    log.LOGW($"[FundamentalSET100::IncomeCutstring] [{main}]: {name} {value}");
            }
            else
            {
                main = name;
                if (name == "รายได้จากการขายและหรือการให้บริการ")
                    income.Revenues_rendering_services = value;
                else if (name == "รายได้อื่น")
                    income.Other_income = value;
                else if (name == "ส่วนแบ่งกำไรจากเงินลงทุนตามวิธีส่วนได้เสีย")
                    income.Shares_investments_accounted = value;
                else if (name == "รวมรายได้")
                    income.Total_revenues = value;
                else if (name == "ต้นทุนขายสินค้าและหรือต้นทุนการให้บริการ")
                    income.Cost_sale_rendering_services = value;
                else if (name == "ค่าใช้จ่ายในการขายและบริหาร")
                    income.Selling_admin_expenses = value;
                else if (name == "ค่าใช้จ่ายอื่น")
                    income.Other_expenses = value;
                else if (name == "รวมค่าใช้จ่าย")
                    income.Total_expenses = value;
                else if (name == "กำไร (ขาดทุน) ก่อนต้นทุนทางการเงิน และภาษีเงินได้")
                    income.Profit_income_tax = value;
                else if (name == "ต้นทุนทางการเงิน")
                    income.Finance_costs = value;
                else if (name == "ภาษีเงินได้")
                    income.Income_tax_expenses = value;
                else if (name == "กำไร (ขาดทุน) สุทธิ")
                    income.Net_profit = value;
                else if (name == "ส่วนของกำไร (ขาดทุน) ที่เป็นของผู้ถือหุ้นบริษัทใหญ่")
                    income.Profit_equity_holders = value;
                else if (name == "ส่วนของกำไร (ขาดทุน) ที่เป็นของส่วนได้เสียที่ไม่มีอำนาจควบคุม")
                    income.Profit_non_controlling = value;
                else if (name == "กำไร (ขาดทุน) ต่อหุ้นขั้นพื้นฐาน (หน่วย : บาท)")
                    income.Basic_earnings_share = value;
                else if (name == "รายได้ดอกเบี้ย")
                    income.Interest = value;
                else if (name == "ค่าใช้จ่ายดอกเบี้ย / กู้ยืม")
                    income.Interest_expense = value;
                else if (name == "รายได้ดอกเบี้ยสุทธิ")
                    income.Net_interest_income = value;
                else if (name == "รายได้ค่าธรรมเนียมและบริการ")
                    income.Fees_service_income = value;
                else if (name == "ค่าใช้จ่ายค่าธรรมเนียมและบริการ")
                    income.Fees_service_expenses = value;
                else if (name == "รายได้ค่าธรรมเนียมและบริการสุทธิ")
                    income.Net_fees_service_income = value;
                else if (name == "กำไร (ขาดทุน) สุทธิจากธุรกรรมเพื่อค้าและปริวรรตเงินตรา")
                    income.Gain_Trading_foreign = value;
                else if (name == "กำไร (ขาดทุน) สุทธิจากเงินลงทุน")
                    income.Gain_selling_investments = value;
                else if (name == "ส่วนแบ่งกำไร (ขาดทุน) จากเงินลงทุนตามวิธีส่วนได้เสีย")
                    income.Share_profit_loss_investment = value;
                else if (name == "รายได้จากการดำเนินการอื่น")
                    income.Other_operating_incomes = value;
                else if (name == "ค่าใช้จ่ายในการดำเนินงานอื่น")
                    income.Other_operating_expenses = value;
                else if (name == "หนี้สูญ หนี้สงสัยจะสูญ และขาดทุนจากการด้อยค่าของเงินให้สินเชื่อและตราสารหนี้")
                    income.Bad_debt_securities = value;
                else if (name == "กำไร (ขาดทุน) ก่อนภาษีเงินได้")
                    income.Before_income_tax_expenses = value;
                else if (name == "กำไร (ขาดทุน) ต่อหุ้นปรับลด (หน่วย : บาท)")
                    income.Diluted_earning_share = value;
                else if (name == "ค่าตอบแทนกรรมการและผู้บริหาร")
                    income.Management_directors_remuneration = value;
                else if (name == "ส่วนแบ่งขาดทุนจากเงินลงทุนตามวิธีส่วนได้เสีย")
                    income.Shares_loss_using_equity = value;
                else if (name == "กำไร (ขาดทุน) สุทธิจากหนี้สินทางการเงินที่กำหนดให้แสดงด้วยมูลค่ายุติธรรม")
                    income.Financial_instrument_designated = value;
                else if (name == "รายการอื่น ๆ")
                    income.Other_item = value;
                else
                    log.LOGW($"[FundamentalSET100::IncomeCutstring] {name} {value}");
            }
        }
        public static void ComprehensiveIncomeCutstring(string name, string value)
        {
            if (name.IndexOf("&nbsp;") > -1)
            {
                if (Regex.Matches(name, "&nbsp;").Count == 3)
                {
                    name = name.Replace("&nbsp;", "");
                    sub = name;
                    if (name == "ส่วนของกำไร (ขาดทุน) เบ็ดเสร็จที่เป็นของผู้ถือหุ้นบริษัทใหญ่" && main == "กำไร (ขาดทุน) เบ็ดเสร็จรวม สำหรับงวด")
                        comprehensive_income.Total_holders_parent = value;
                    else if (name == "ส่วนของกำไร (ขาดทุน) เบ็ดเสร็จที่เป็นของส่วนได้เสียที่ไม่มีอำนาจควบคุม" && main == "กำไร (ขาดทุน) เบ็ดเสร็จรวม สำหรับงวด")
                        comprehensive_income.Total_non_controlling = value;
                    else
                        log.LOGW($"[FundamentalSET100::ComprehensiveIncomeCutstring] [{main}]: {name} {value}");
                }
                else
                    log.LOGW($"[FundamentalSET100::ComprehensiveIncomeCutstring] [{main}]: {name} {value}");
            }
            else
            {
                main = name;
                if (name == "กำไร (ขาดทุน) สำหรับงวด")
                    comprehensive_income.Net_profit = value;
                else if (name == "กำไร (ขาดทุน) ที่ยังไม่เกิดขึ้นจากการวัดมูลค่าสินทรัพย์ทางการเงินเผื่อขาย")
                    comprehensive_income.Unrealised_gain = value;
                else if (name == "กำไร (ขาดทุน) จากการประมาณการตามหลักคณิตศาสตร์ประกันภัยสำหรับโครงการผลประโยชน์พนักงาน")
                    comprehensive_income.Actuarial_gain = value;
                else if (name == "กำไร (ขาดทุน) จากการประเมินมูลค่ายุติธรรมตราสารป้องกันความเสี่ยงจากในกระแสเงินสด")
                    comprehensive_income.Gain_cash_flow = value;
                else if (name == "ผลต่างอัตราแลกเปลี่ยนจากการแปลงค่างบการเงินจากการดำเนินงานในต่างประเทศ")
                    comprehensive_income.Exchange_differces = value;
                else if (name == "ส่วนแบ่งกำไร (ขาดทุน) เบ็ดเสร็จอื่นในบริษัทร่วม")
                    comprehensive_income.Share_other_comprehensive = value;
                else if (name == "ภาษีเงินได้เกี่ยวกับองค์ประกอบของกำไร (ขาดทุน) เบ็ดเสร็จอื่น")
                    comprehensive_income.Income_tax_relating = value;
                else if (name == "กำไร (ขาดทุน) เบ็ดเสร็จรวม สำหรับงวด")
                    comprehensive_income.Total_comprehensive_income = value;
                else if (name == "การเปลี่ยนแปลงในส่วนเกินทุนจากการตีราคาสินทรัพย์")
                    comprehensive_income.Change_assets_revaluation = value;
                else if (name == "กำไร (ขาดทุน) เบ็ดเสร็จอื่น - อื่น ๆ")
                    comprehensive_income.Other_comprehensive_income = value;
                else if (name == "กำไร (ขาดทุน) จากการประเมินมูลค่ายุติธรรมตราสารป้องกันความเสี่ยงสำหรับป้องกันความเสี่ยงในเงินลงทุนสุทธิในหน่วยงานต่างประเทศ")
                    comprehensive_income.Hedges_foreign_operations = value;
                else
                    log.LOGW($"[FundamentalSET100::ComprehensiveIncomeCutstring] {name} {value}");
            }
        }
        public static void CashFlowCutstring(string name, string value)
        {
            if (name.IndexOf("&nbsp;") > -1)
            {
                if (Regex.Matches(name, "&nbsp;").Count == 3)
                {
                    name = name.Replace("&nbsp;", "");
                    sub = name;
                    if (name == "ลูกหนี้การค้าและลูกหนี้อื่น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Trade_account = value;
                    else if (name == "ลูกหนี้อื่น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Other_receivables = value;
                    else if (name == "สินค้าคงเหลือ (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Inventories = value;
                    else if (name == "สินทรัพย์หมุนเวียนอื่น (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Other_current_assets = value;
                    else if (name == "สินทรัพย์ไม่หมุนเวียนอื่น (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Other_non_current_assets = value;
                    else if (name == "เจ้าหนี้การค้าและเจ้าหนี้อื่น - บุคคลหรือกิจการอื่น เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Trade_account_payables = value;
                    else if (name == "เจ้าหนี้อื่น - บุคคลหรือกิจการอื่น เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Other_payables = value;
                    else if (name == "หนี้สินหมุนเวียนอื่น เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Other_current_liabilities = value;
                    else if (name == "หนี้สินไม่หมุนเวียนอื่น เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Other_non_current_liabilities = value;
                    else if (name == "เงินลงทุนระยะยาว (เพิ่มขึ้น)" && main == "เงินลงทุนระยะยาว (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_long_investments = value;
                    else if (name == "เงินลงทุนระยะยาวลดลง" && main == "เงินลงทุนระยะยาว (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_long_investments = value;
                    else if (name == "เงินลงทุนในบริษัทย่อย และ/หรือ บริษัทร่วม (เพิ่มขึ้น)" && main == "เงินลงทุนในบริษัทย่อย และ/หรือ บริษัทร่วม (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_investment_subsidiaries = value;
                    else if (name == "เงินลงทุนในบริษัทย่อย และ/หรือ บริษัทร่วมลดลง" && main == "เงินลงทุนในบริษัทย่อย และ/หรือ บริษัทร่วม (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_investment_subsidiaries = value;
                    else if (name == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการอื่น (เพิ่มขึ้น)" && main == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_long_loans = value;
                    else if (name == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการอื่นลดลง" && main == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_long_loans = value;
                    else if (name == "เงินสดรับจากการขายที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ (เพิ่มขึ้น) ลดลง")
                        cash_flow.Proceeds_disposal = value;
                    else if (name == "เงินสดจ่ายจากการซื้อที่ดิน อาคาร และอุปกรณ์" && main == "ที่ดิน อาคาร และอุปกรณ์ (เพิ่มขึ้น) ลดลง")
                        cash_flow.Purchases_property = value;
                    else if (name == "สินทรัพย์ไม่มีตัวตน (เพิ่มขึ้น)" && main == "สินทรัพย์ไม่มีตัวตน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_intangible_assets = value;
                    else if (name == "สินทรัพย์ภายใต้สัญญาสัมปทาน (เพิ่มขึ้น)" && main == "สินทรัพย์ภายใต้สัญญาสัมปทาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_assets_under_concession = value;
                    else if (name == "เงินกู้ยืมระยะยาวจากสถาบันการเงินเพิ่มขึ้น" && main == "เงินกู้ยืมระยะยาวจากสถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Increase_long_borrowings = value;
                    else if (name == "เงินกู้ยืมระยะยาวจากสถาบันการเงิน (ลดลง)" && main == "เงินกู้ยืมระยะยาวจากสถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Decrease_long_borrowings = value;
                    else if (name == "หนี้สินตามสัญญาเช่าการเงินและเช่าซื้อ (ลดลง)" && main == "หนี้สินตามสัญญาเช่าการเงินและเช่าซื้อ เพิ่มขึ้น (ลดลง)")
                        cash_flow.Decrease_finance_lease = value;
                    else if (name == "ลูกหนี้อื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Other_receivables_related = value;
                    else if (name == "เจ้าหนี้อื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Other_payables_related = value;
                    else if (name == "เงินกู้ยืมระยะยาวจากบุคคลหรือกิจการอื่น (ลดลง)" && main == "เงินกู้ยืมระยะยาวจากบุคคลหรือกิจการอื่น เพิ่มขึ้น (ลดลง)")
                        cash_flow.Decrease_long_borrowings_parties = value;
                    else if (name == "เงินสดรับจากการจำหน่ายตราสารหนี้" && (main == "ตราสารหนี้ เพิ่มขึ้น (ลดลง)" || main == "ตราสารหนี้ (เพิ่มขึ้น) ลดลง"))
                        cash_flow.Proceeds_issuance_debt = value;
                    else if (name == "ค่าเสื่อมราคา" && main == "ค่าเสื่อมราคาและค่าตัดจำหน่าย")
                        cash_flow.Depreciation = value;
                    else if (name == "ค่าตัดจำหน่าย" && main == "ค่าเสื่อมราคาและค่าตัดจำหน่าย")
                        cash_flow.Amortisation = value;
                    else if (name == "เงินกู้ยืมระยะยาวจากบุคคลหรือกิจการอื่นเพิ่มขึ้น" && main == "เงินกู้ยืมระยะยาวจากบุคคลหรือกิจการอื่น เพิ่มขึ้น (ลดลง)")
                        cash_flow.Increase_long_borrowings_parties = value;
                    else if (name == "เงินลงทุนอื่น (เพิ่มขึ้น)" && main == "เงินลงทุนอื่น (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_other_investment = value;
                    else if (name == "เงินให้สินเชื่อ (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_loans = value;
                    else if (name == "เงินสดจ่ายชำระคืนตราสารหนี้" && (main == "ตราสารหนี้ เพิ่มขึ้น (ลดลง)" || main == "ตราสารหนี้ (เพิ่มขึ้น) ลดลง"))
                        cash_flow.Repayment_debentures = value;
                    else if (name == "ดอกเบี้ยรับ" && main == "เงินปันผลและดอกเบี้ยรับ")
                        cash_flow.Interest_received_income = value;
                    else if (name == "อสังหาริมทรัพย์เพื่อการลงทุน (เพิ่มขึ้น)" && main == "อสังหาริมทรัพย์เพื่อการลงทุน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_investment_properties = value;
                    else if (name == "อสังหาริมทรัพย์เพื่อการลงทุนลดลง" && main == "อสังหาริมทรัพย์เพื่อการลงทุน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_investment_properties = value;
                    else if (name == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น)" && main == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_long_loans_related = value;
                    else if (name == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกันลดลง" && main == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_loan_loans_related = value;
                    else if (name == "ตราสารหนี้ลดลง" && (main == "ตราสารหนี้ เพิ่มขึ้น (ลดลง)" || main == "ตราสารหนี้ (เพิ่มขึ้น) ลดลง"))
                        cash_flow.Decrease_debt_instruments = value;
                    else if (name == "เงินปันผลรับ" && main == "เงินปันผลและดอกเบี้ยรับ")
                        cash_flow.Dividend_received_income = value;
                    else if (name == "เงินทดรองจ่ายและเงินให้กู้ยืมระยะสั้นแก่บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Advances_short_loans_partie = value;
                    else if (name == "รายการระหว่างธนาคารและตลาดเงิน (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Interbank_money_market = value;
                    else if (name == "เงินลงทุนชั่วคราว (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Short_investments = value;
                    else if (name == "ทรัพย์สินรอการขาย (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Properties_foreclosed_assets = value;
                    else if (name == "เงินรับฝาก เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Deposits = value;
                    else if (name == "รายการระหว่างธนาคารและตลาดเงิน เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Interbank_money_operating = value;
                    else if (name == "หนี้สินจ่ายคืนเมื่อทวงถาม เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Liabilities_payable_demand = value;
                    else if (name == "เงินกู้ยืม เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Borrowings_operating = value;
                    else if (name == "ลูกหนี้การค้าและลูกหนี้อื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Trade_account_receivables = value;
                    else if (name == "เจ้าหนี้การค้าและเจ้าหนี้อื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Trade_account_payables_related = value;
                    else if (name == "เงินลงทุนอื่นลดลง" && main == "เงินลงทุนอื่น (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_other_investment = value;
                    else if (name == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น)" && main == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_other_loans = value;
                    else if (name == "สินทรัพย์ไม่มีตัวตนลดลง" && main == "สินทรัพย์ไม่มีตัวตน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_intangible_assets = value;
                    else if (name == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกันลดลง" && main == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_other_loans = value;
                    else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน (ลดลง)" && main == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Decrease_long_borrowings_related = value;
                    else if (name == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการอื่นลดลง" && main == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                        cash_flow.Decrease_other_loans_parties = value;
                    else if (name == "ลูกหนี้สำนักหักบัญชี (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Receivables_clearing_house = value;
                    else if (name == "ลูกหนี้ธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Securities_derivatives_business_operating = value;
                    else if (name == "ภาษีเงินได้นิติบุคคลค้างจ่าย เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Income_tax_payable = value;
                    else if (name == "ประมาณการหนี้สิน เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Provisions = value;
                    else if (name == "ค่าใช้จ่ายค้างจ่าย เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Accrued_expenses = value;
                    else if (name == "เจ้าหนี้สำนักหักบัญชี เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Payables_clearing_house = value;
                    else if (name == "เจ้าหนี้ธุรกิจหลักทรัพย์และสัญญาซื้อขายล่วงหน้า เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Securities_derivatives_business = value;
                    else if (name == "ภาษีเงินได้นิติบุคคลค้างจ่าย เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Income_tax_payable_operating = value;
                    else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกันเพิ่มขึ้น" && main == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Increase_long_borrowings_related = value;
                    else if (name == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น)" && main == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                        cash_flow.Increase_loan_other_parties = value;
                    else if (name == "เงินเบิกเกินบัญชี / เงินกู้ยืมอื่น - สถาบันการเงินเพิ่มขึ้น" && main == "เงินเบิกเกินบัญชี / เงินกู้ยืมอื่น - สถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Increase_loan_financial = value;
                    else if (name == "เงินเบิกเกินบัญชี / เงินกู้ยืมอื่น - สถาบันการเงิน (ลดลง)" && main == "เงินเบิกเกินบัญชี / เงินกู้ยืมอื่น - สถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Decrease_loan_financial = value;
                    else if (name == "เงินกู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกันเพิ่มขึ้น" && main == "เงินกู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Increase_loans_related = value;
                    else if (name == "รายได้ค้างรับ (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Accrued_income = value;
                    else if (name == "สินทรัพย์ตราสารอนุพันธ์ (เพิ่มขึ้น) ลดลง" && main == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                        cash_flow.Other_derivatives_assets = value;
                    else if (name == "หนี้สินตราสารอนุพันธ์ เพิ่มขึ้น (ลดลง)" && main == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                        cash_flow.Derivatives_liabilities = value;
                    else if (name == "หนี้สินตามสัญญาเช่าการเงินและเช่าซื้อเพิ่มขึ้น" && main == "หนี้สินตามสัญญาเช่าการเงินและเช่าซื้อ เพิ่มขึ้น (ลดลง)")
                        cash_flow.Increase_finance_lease = value;
                    else
                        log.LOGW($"[FundamentalSET100::CashFlowCutstring] [{main}]: {name} {value}");
                }
                else
                    log.LOGW($"[FundamentalSET100::CashFlowCutstring] [{main}]: {name} {value}");
            }
            else
            {
                main = name;
                if (name == "กำไร (ขาดทุน) สุทธิสำหรับงวด / ของบริษัทใหญ่")
                    cash_flow.Period_net_profit = value;
                else if (name == "ค่าเสื่อมราคาและค่าตัดจำหน่าย")
                    cash_flow.Depreciation_amortisation = value;
                else if (name == "หนี้สูญและหนี้สงสัยจะสูญ (โอนกลับ)")
                    cash_flow.Bad_debt_doubleful = value;
                else if (name == "ขาดทุนจากสินค้าล้าสมัย / เสื่อมสภาพ (โอนกลับ)")
                    cash_flow.Obsolescence = value;
                else if (name == "ขาดทุนจากการตีราคาสินค้าคงเหลือ (โอนกลับ)")
                    cash_flow.Diminotion_value_inventories = value;
                else if (name == "กำไร (ขาดทุน) สุทธิส่วนที่เป็นของส่วนได้เสียที่ไม่มีอำนาจควบคุม")
                    cash_flow.Attributable_non_controlling = value;
                else if (name == "ส่วนแบ่ง (กำไร) ขาดทุนจากเงินลงทุนตามวิธีส่วนได้เสีย")
                    cash_flow.Share_investments_accounted = value;
                else if (name == "(กำไร) ขาดทุนที่ยังไม่เกิดขึ้นจากอัตราแลกเปลี่ยน")
                    cash_flow.Unrealised_foreign_exchange = value;
                else if (name == "ขาดทุนจากการด้อยค่าของสินทรัพย์อื่น (โอนกลับ)")
                    cash_flow.Impairment_other_assets = value;
                else if (name == "(กำไร) ขาดทุนจากการขายเงินลงทุนในบริษัทย่อย และ/หรือ บริษัทร่วม")
                    cash_flow.Sales_investments_subsidiaries = value;
                else if (name == "(กำไร) ขาดทุนจากการขายสินทรัพย์ถาวร")
                    cash_flow.Disposal_fixed_assets = value;
                else if (name == "(กำไร) ขาดทุนจากการเปลี่ยนแปลงมูลค่ายุติธรรมของสินทรัพย์อื่น")
                    cash_flow.Fair_value_adjustments = value;
                else if (name == "ต้นทุนทางการเงิน")
                    cash_flow.Finance_costs = value;
                else if (name == "ภาษีเงินได้")
                    cash_flow.Income_tax_expenses = value;
                else if (name == "รายการปรับปรุงอื่น ๆ")
                    cash_flow.Other_reconciliation_items = value;
                else if (name == "เงินสดได้มาจาก (ใช้ไปใน) การดำเนินงานก่อนการเปลี่ยนแปลงในสินทรัพย์และหนี้สินดำเนินงาน")
                    cash_flow.Cash_flow_operations = value;
                else if (name == "สินทรัพย์ดำเนินงาน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Operating_assets = value;
                else if (name == "หนี้สินดำเนินงาน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Operating_liabilities = value;
                else if (name == "เงินสดรับ (จ่าย) จากการดำเนินงาน")
                    cash_flow.Cash_generated_operations = value;
                else if (name == "จ่ายภาษีเงินได้")
                    cash_flow.Income_tax_paid = value;
                else if (name == "เงินสดสุทธิได้มาจาก (ใช้ไปใน) กิจกรรมดำเนินงาน")
                    cash_flow.Net_cash_provided = value;
                else if (name == "เงินลงทุนระยะสั้น (เพิ่มขึ้น) ลดลง")
                    cash_flow.Short_term_investments = value;
                else if (name == "เงินลงทุนระยะยาว (เพิ่มขึ้น) ลดลง")
                    cash_flow.Long_term_investments = value;
                else if (name == "เงินลงทุนในบริษัทย่อย และ/หรือ บริษัทร่วม (เพิ่มขึ้น) ลดลง")
                    cash_flow.Investment_subsidiaries = value;
                else if (name == "เงินทดรองและเงินให้กู้ยืมระยะสั้น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                    cash_flow.Advances_short_loans = value;
                else if (name == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                    cash_flow.Long_term_loans = value;
                else if (name == "ที่ดิน อาคาร และอุปกรณ์ (เพิ่มขึ้น) ลดลง")
                    cash_flow.Property_plant_equipments = value;
                else if (name == "สินทรัพย์ไม่มีตัวตน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Intangible_assets = value;
                else if (name == "สินทรัพย์ภายใต้สัญญาสัมปทาน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Assets_under_concession = value;
                else if (name == "เงินปันผลรับ")
                    cash_flow.Dividends_received = value;
                else if (name == "ดอกเบี้ยรับ")
                    cash_flow.Interest_received = value;
                else if (name == "รายการอื่น ๆ" && last == "ดอกเบี้ยรับ")
                    cash_flow.Other_item_received = value;
                else if (name == "เงินสดสุทธิได้มาจาก (ใช้ไปใน) กิจกรรมลงทุน")
                    cash_flow.Net_provided_investing = value;
                else if (name == "เงินเบิกเกินบัญชีและเงินกู้ยืมระยะสั้น - สถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Short_borrowing_financial = value;
                else if (name == "เงินกู้ยืมระยะยาวจากสถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Long_borrowing_financial = value;
                else if (name == "หนี้สินตามสัญญาเช่าการเงินและเช่าซื้อ เพิ่มขึ้น (ลดลง)")
                    cash_flow.Finance_lease_contract = value;
                else if (name == "เงินสดรับจากการจำหน่ายหุ้นทุน")
                    cash_flow.Proceeds_issuance = value;
                else if (name == "เงินปันผลจ่าย")
                    cash_flow.Dividend_paid = value;
                else if (name == "ดอกเบี้ยจ่าย")
                    cash_flow.Interest_paid = value;
                else if (name == "รายการอื่น ๆ" && last == "ดอกเบี้ยจ่าย")
                    cash_flow.Other_item_paid = value;
                else if (name == "เงินสดสุทธิได้มาจาก (ใช้ไปใน) กิจกรรมจัดหาเงิน")
                    cash_flow.Net_provided_financing = value;
                else if (name == "เงินสดและรายการเทียบเท่าเงินสด เพิ่มขึ้น (ลดลง) สุทธิ")
                    cash_flow.Net_increase_cash = value;
                else if (name == "ผลกระทบจากอัตราแลกเปลี่ยนในเงินสดและรายการเทียบเท่าเงินสด")
                    cash_flow.Effect_exchange_rate = value;
                else if (name == "ผลต่างจากการแปลงค่างบการเงินที่เป็นเงินตราต่างประเทศ เพิ่มขึ้น (ลดลง)")
                    cash_flow.Differences_financial_translation = value;
                else if (name == "เงินสดและรายการเทียบเท่าเงินสด ต้นงวด")
                    cash_flow.Cash_beginning_balance = value;
                else if (name == "เงินสดและรายการเทียบเท่าเงินสด สิ้นงวด")
                    cash_flow.Cash_ending_balance = value;
                else if (name == "กำไร (ขาดทุน) ก่อนต้นทุนทางการเงิน และ/หรือ ภาษีเงินได้")
                    cash_flow.Before_financial_costs = value;
                else if (name == "เงินกู้ยืมระยะยาวจากบุคคลหรือกิจการอื่น เพิ่มขึ้น (ลดลง)")
                    cash_flow.Long_borrowing_parties = value;
                else if (name == "ตราสารหนี้ เพิ่มขึ้น (ลดลง)" || name == "ตราสารหนี้ (เพิ่มขึ้น) ลดลง")
                    cash_flow.Debt_instruments = value;
                else if (name == "ขาดทุนจากการด้อยค่าของสินทรัพย์ถาวร (โอนกลับ)")
                    cash_flow.Impairment_loss_fixed_assets = value;
                else if (name == "(กำไร) ขาดทุนจากการขายสินทรัพย์อื่น")
                    cash_flow.Disposal_other_assets = value;
                else if (name == "เงินลงทุนอื่น (เพิ่มขึ้น) ลดลง")
                    cash_flow.Other_investment = value;
                else if (name == "ขาดทุนจากการตัดจำหน่ายสินทรัพย์ถาวร")
                    cash_flow.Loss_write_fixed = value;
                else if (name == "เงินปันผลและดอกเบี้ยรับ")
                    cash_flow.Dividend_interest_income = value;
                else if (name == "รับดอกเบี้ย")
                    cash_flow.Interest_received_s = value;
                else if (name == "จ่ายดอกเบี้ย")
                    cash_flow.Interest_paid_s = value;
                else if (name == "รายการอื่น ๆ" && last == "เงินปันผลจ่าย")
                    cash_flow.Other_item_dividend = value;
                else if (name == "(กำไร) ขาดทุนจากการขายทรัพย์สินรอการขาย")
                    cash_flow.Disposal_properties_foreclosed = value;
                else if (name == "อสังหาริมทรัพย์เพื่อการลงทุน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Investment_properties = value;
                else if (name == "เงินกู้ยืมระยะสั้น - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Short_borrowing_related = value;
                else if (name == "(กำไร) ขาดทุนจากการขายเงินลงทุนอื่น")
                    cash_flow.Disposal_other_investments = value;
                else if (name == "(กำไร) ขาดทุนจากการเปลี่ยนแปลงมูลค่ายุติธรรมของอสังหาริมทรัพย์เพื่อการลงทุน")
                    cash_flow.Fair_adjustments_investments = value;
                else if (name == "เงินทดรองและเงินให้กู้ยืมระยะสั้น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Advances_short_loans_related = value;
                else if (name == "เงินให้กู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Long_loans_related = value;
                else if (name == "เงินฝากสถาบันการเงินที่มีข้อจำกัดการใช้ (เพิ่มขึ้น) ลดลง")
                    cash_flow.Restricted_deposits_financial = value;
                else if (name == "รายการอื่น ๆ")
                    cash_flow.Other_item = value;
                else if (name == "ขาดทุนจากการตัดจำหน่ายสินทรัพย์อื่น")
                    cash_flow.Writeoff_other_assets = value;
                else if (name == "(กำไร) ขาดทุนจากการเปลี่ยนแปลงมูลค่ายุติธรรมของเงินลงทุน")
                    cash_flow.Fair_value_adjustments_investments = value;
                else if (name == "(กำไร) ขาดทุนจากการโอนเปลี่ยนประเภทเงินลงทุน")
                    cash_flow.Reclassification_investments = value;
                else if (name == "ขาดทุนจากการด้อยค่าของเงินลงทุน (โอนกลับ)")
                    cash_flow.Impairment_loss_investment = value;
                else if (name == "ขาดทุนจากการด้อยค่าของทรัพย์สินรอการขาย (โอนกลับ)")
                    cash_flow.Impairment_loss_properties = value;
                else if (name == "(กำไร) ขาดทุนจากการปรับโครงสร้างหนี้")
                    cash_flow.Debt_restructuring = value;
                else if (name == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน (เพิ่มขึ้น) ลดลง")
                    cash_flow.Other_loans_related = value;
                else if (name == "ขาดทุนจากการตัดจำหน่ายสินทรัพย์ไม่มีตัวตน")
                    cash_flow.Writeoff_intangible_assets = value;
                else if (name == "เงินสดจ่ายเพื่อซื้อหุ้นทุนซื้อคืน")
                    cash_flow.Purchase_treasury_shares = value;
                else if (name == "เงินกู้ยืมระยะยาว - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Long_borrowing_related = value;
                else if (name == "เงินสดรับล่วงหน้าค่าหุ้น")
                    cash_flow.Proceeds_advance = value;
                else if (name == "เงินทดรอง / เงินให้กู้ยืมอื่น - บุคคลหรือกิจการอื่น (เพิ่มขึ้น) ลดลง")
                    cash_flow.Other_loan_other_parties = value;
                else if (name == "ขาดทุนจากการตัดจำหน่ายสินทรัพย์ภายใต้สัญญาสัมปทาน")
                    cash_flow.Loss_writoff_assets = value;
                else if (name == "ขาดทุนจากการด้อยค่าของสินทรัพย์ไม่มีตัวตน (โอนกลับ)")
                    cash_flow.Impairment_loss_identifiable = value;
                else if (name == "ทรัพย์สินรอการขาย (เพิ่มขึ้น) ลดลง")
                    cash_flow.Properties_foreclosed = value;
                else if (name == "(กำไร) ขาดทุนจากการขายสินทรัพย์ไม่มีตัวตน")
                    cash_flow.Disposal_intangible_assets = value;
                else if (name == "เงินเบิกเกินบัญชี / เงินกู้ยืมอื่น - สถาบันการเงิน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Other_loan_financial = value;
                else if (name == "เงินกู้ยืมระยะสั้น - บุคคลหรือกิจการอื่น เพิ่มขึ้น (ลดลง)")
                    cash_flow.Short_borrowing_other_parties = value;
                else if (name == "เงินกู้ยืมอื่น - บุคคลหรือกิจการที่เกี่ยวข้องกัน เพิ่มขึ้น (ลดลง)")
                    cash_flow.Other_loans_related_parties = value;
                else if (name == "รับเงินปันผล")
                    cash_flow.Dividend_received = value;
                else if (name == "(กำไร) ขาดทุนจากการขายตราสารอนุพันธ์")
                    cash_flow.Disposal_derivative_trading = value;
                else if (name == "ขาดทุนจากการตัดจำหน่ายเงินลงทุน")
                    cash_flow.Loss_write_investments = value;
                else if (name == "ขาดทุนจากการตัดจำหน่ายทรัพย์สินรอการขาย")
                    cash_flow.Loss_write_properties_foreclosed = value;
                else if (name == "จ่ายเงินปันผล")
                    cash_flow.Dividend_paid_s = value;
                else if (name == "เจ้าหนี้ทรัสต์รีซีท เพิ่มขึ้น (ลดลง)")
                    cash_flow.Liabilities_under_trust = value;
                else
                    log.LOGW($"[FundamentalSET100::CashFlowCutstring] [{name} {value}");
            }
            last = name;
        }
    }
}
