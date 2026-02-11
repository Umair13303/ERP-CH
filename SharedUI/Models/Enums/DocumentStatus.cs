using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedUI.Models.Enums
{
    public enum DocumentStatus
    {
        active_company = 1,
        inactive_company = 2,
        deleted_company = 3,

        active_user = 4,
        inactive_user = 5,
        deleted_user = 6,

        active_right_setting = 7,
        inactive_right_setting = 8,

        active_user_right = 9,
        inactive_user_right = 10,

        active_branch = 11,
        inactive_branch = 12,
        deleted_branch = 13,

        active_branch_setting = 14,
        inactive_branch_setting = 15,
        expired_branch_setting = 16,
        deleted_branch_setting = 17,

        active_academic_class = 18,
        inactive_academic_class = 19,
        deleted_academic_class = 20,

        active_academic_admission_session = 21,
        inactive_academic_admission_session = 22,
        deleted_academic_admission_session = 23,

        active_academic_subject = 24,
        inactive_academic_subject = 25,
        deleted_academic_subject = 26,

        active_academic_class_curriculum = 27,
        inactive_academic_class_curriculum = 28,
        deleted_academic_class_curriculum = 29,

        active_academic_class_curriculum_subject = 30,
        inactive_academic_class_curriculum_subject = 31,
        deleted_academic_class_curriculum_subject = 32,

        active_academic_admission_session_class = 33,
        inactive_academic_admission_session_class = 34,
        deleted_academic_admission_session_class = 35,

        active_account_chart_of_account = 36,
        inactive_account_chart_of_account = 37,
        deleted_account_chart_of_account = 38,

        active_account_discount_type = 39,
        inactive_account_discount_type = 40,
        deleted_account_discount_type = 41,

        active_account_fee_type = 42,
        inactive_account_fee_type = 43,
        deleted_account_fee_type = 44,

        active_account_class_fee_structure = 45,
        inactive_account_class_fee_structure = 46,
        deleted_account_class_fee_structure = 47,

        active_account_class_fee_structure_fee_type = 45,
        inactive_account_class_fee_structure_fee_type = 46,
        deleted_account_class_fee_structure_fee_type = 47,

        active_non_registered_student = 48,
        active_registered_student = 49,
        inactive_non_registered_student = 50,
        inactive_registered_student = 51,
        deleted_non_registered_student = 52,
        deleted_registered_student = 53,
    }
}
