namespace CrmBackend.Domain.Enums;

public enum InquiryStatus
{
    // --------------------- Measurement ---------------------
    MeasurementInProgress = 1,
    MeasurementDelayed = 2,
    MeasurementAccepted = 7,
    MeasurementRejected = 8,
    MeasurementWaitingForApproval = 9,
    MeasurementAssigneePending = 36,
    MeasurementAssigneeAccepted = 37,
    MeasurementAssigneeRejected = 38,
    MeasurementAssigneeDelayed = 73,
    WaitingForMeasurementFees = 31,

    // --------------------- Design ---------------------
    DesignPending = 3,
    DesignDelayed = 4,
    DesignAccepted = 10,
    DesignRejected = 11,
    DesignWaitingForApproval = 12,
    DesignWaitingForCustomerApproval = 16,
    DesignRejectedByCustomer = 17,
    DesignAssigneePending = 39,
    DesignAssigneeAccepted = 40,
    DesignAssigneeRejected = 41,
    DesignDelayedOnCustomerApproval = 79,

    // --------------------- Quotation ---------------------
    QuotationPending = 5,
    QuotationDelayed = 6,
    QuotationAccepted = 13,
    QuotationRejected = 14,
    QuotationWaitingForCustomerApproval = 15,
    QuotationSchedulePending = 42,
    QuotationWaitingForApproval = 57,
    QuotationRevisionRequested = 61,
    QuotationScheduleDelay = 91,

    // --------------------- Checklist ---------------------
    ChecklistPending = 18,
    ChecklistAccepted = 19,
    ChecklistRejected = 20,
    CommercialChecklistPending = 43,
    CommercialChecklistAccepted = 44,
    CommercialChecklistRejected = 45,

    // --------------------- Contract ---------------------
    ContractPending = 58,
    ContractWaitingForCustomerApproval = 59,
    ContractApproved = 60,
    ContractInProgress = 62,
    ContractRejected = 63,

    // --------------------- Job Order ---------------------
    JobOrderPending = 21,
    JobOrderCreated = 22,
    JobOrderApproved = 23,
    JobOrderRejected = 24,
    JobOrderWaitingForApproval = 25,
    JobOrderDelayed = 26,
    JobOrderCompleted = 27,
    JobOrderConfirmationPending = 46,
    JobOrderInProgress = 47,
    JobOrderFactoryRejected = 48,
    JobOrderRescheduleRequested = 49,
    JobOrderRescheduleApproved = 50,
    JobOrderRescheduleRejected = 51,
    JobOrderDelayRequested = 52,
    JobOrderReadyForInstallation = 53,
    JobOrderFilesPending = 54,
    JobOrderFilesDelayed = 55,
    JobOrderAuditPending = 64,
    JobOrderAuditRejected = 65,
    JobOrderAuditDelay = 78,
    JobOrderFactoryDelayed = 68,
    JobOrderProcurementDelayed = 69,
    JobOrderDesignTeamDelay = 71,
    JobOrderFactoryConfirmationDelayed = 77,
    JobOrderSiteNotReady = 70,

    // --------------------- Site Verification ---------------------
    JobOrderSiteVerificationAssigneePending = 80,
    JobOrderSiteVerificationPending = 81,
    JobOrderSiteVerificationDelayed = 82,
    JobOrderSiteVerificationRejected = 83,

    // --------------------- Job Order Assignment ---------------------
    JobOrderAssigneePending = 84,
    JobOrderAssigneeDelayed = 85,
    JobOrderAssigneeRejected = 86,

    // --------------------- Installation Date ---------------------
    JobOrderInstallationDatePending = 87,
    JobOrderInstallationDateDelay = 88,
    JobOrderInstallationDateRejected = 89,

    // --------------------- Detailed Files ---------------------
    DetailedFilesPending = 72,
    DetailedFilesSchedulePending = 75,
    DetailedFilesDelayed = 76,

    // --------------------- Special Approvals ---------------------
    SpecialApprovalPending = 66,
    SpecialApprovalRejected = 67,

    // --------------------- Payments ---------------------
    WaitingForAdvance = 32,
    WaitingForBeforeDeliveryPayment = 33,
    WaitingForAfterDeliveryPayment = 34,

    // --------------------- Delivery ---------------------
    DeliveryPending = 28,
    UnableToDeliver = 29,
    Delivered = 30,

    // --------------------- Completion ---------------------
    InquiryCompleted = 35,
    Closed = 90
}
