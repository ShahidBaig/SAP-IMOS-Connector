// -----------------------------------------------------------------------
// <copyright file="AccountType.cs" company="Visionary Computer Solutions">
// © 2014 Visionary Computer Solutions Pvt. Ltd. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Data;

namespace IMW.DB
{

    /// <summary>
/// TODO: Update summary.
/// </summary>
    public interface IDBEntity
    {
        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        bool GetList(string Criteria, string Fields, ref DataTable Data, string OrderBy = "");

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        bool GetViewList(string Criteria, string Fields, ref DataTable Data, string OrderBy = "");

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        int GetMax();

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        Result GetObjectFromQuery(string Query);

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        Result GetObject();

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        Result SaveNew();

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        Result Modify();

        /// <summary>
    /// TODO: Update summary.
    /// </summary>
        Result Delete();
    }
}