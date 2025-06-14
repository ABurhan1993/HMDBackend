﻿namespace CrmBackend.Domain.Constants;

public static class PermissionConstants
{
    public static class Customers
    {
        public const string View = "Permissions.Customers.View";
        public const string Create = "Permissions.Customers.Create";
        public const string Edit = "Permissions.Customers.Edit";
        public const string Delete = "Permissions.Customers.Delete";

        public static readonly string[] All = { View, Create, Edit, Delete };
    }

    public static class Users
    {
        public const string View = "Permissions.Users.View";
        public const string Create = "Permissions.Users.Create";
        public const string Edit = "Permissions.Users.Edit";
        public const string Delete = "Permissions.Users.Delete";

        public static readonly string[] All = { View, Create, Edit, Delete };
    }

    public static class Branches
    {
        public const string View = "Permissions.Branches.View";
        public const string Create = "Permissions.Branches.Create";
        public const string Edit = "Permissions.Branches.Edit";
        public const string Delete = "Permissions.Branches.Delete";

        public static readonly string[] All = { View, Create, Edit, Delete };
    }

    public static class Roles
    {
        public const string View = "Permissions.Roles.View";
        public const string Create = "Permissions.Roles.Create";
        public const string Edit = "Permissions.Roles.Edit";
        public const string Delete = "Permissions.Roles.Delete";

        public static readonly string[] All = { View, Create, Edit, Delete };
    }

    public static class CustomerComments
    {
        public const string View = "Permissions.CustomerComments.View";
        public const string Create = "Permissions.CustomerComments.Create";
        public const string Edit = "Permissions.CustomerComments.Edit";
        public const string Delete = "Permissions.CustomerComments.Delete";

        public static readonly string[] All = { View, Create, Edit, Delete };
    }
    public static class Inquiries
    {
        public const string View = "Permissions.Inquiries.View";
        public const string Create = "Permissions.Inquiries.Create";
        public const string Edit = "Permissions.Inquiries.Edit";
        public const string Delete = "Permissions.Inquiries.Delete";

        public static readonly string[] All = { View, Create, Edit, Delete };
    }
    public static class UserClaims
    {
        public const string View = "Permissions.UserClaims.View";
        public const string Create = "Permissions.UserClaims.Create";
        public const string Delete = "Permissions.UserClaims.Delete";

        public static readonly string[] All = { View, Create, Delete };
    }
    public static class Measurements
    {
        public const string View = "Permissions.Measurements.View";
        public const string Assign = "Permissions.Measurements.Assign";
        public const string Approve = "Permissions.Measurements.Approve";

        public static readonly string[] All = { View, Assign, Approve };
    }

    // ✅ جمع كل الصلاحيات العامة
    public static readonly string[] All =
        Customers.All
        .Concat(Users.All)
        .Concat(Branches.All)
        .Concat(Roles.All)
        .Concat(CustomerComments.All)
        .Concat (Inquiries.All)
        .Concat(UserClaims.All)
        .Concat(Measurements.All)
        .ToArray();
}
