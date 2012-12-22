// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLib.validate.unobstrusive.js" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// <note>
//   Depends on jquery.validate.unobtrusive.js
//   Depends on NLib.validate.js
// </note
// --------------------------------------------------------------------------------------------------------------------

(function ($) {
    function setValidationValues(options, ruleName, value) {
        options.rules[ruleName] = value;
        if (options.message) {
            options.messages[ruleName] = options.message;
        }
    }

    function getModelPrefix(fieldName) {
        return fieldName.substr(0, fieldName.lastIndexOf(".") + 1);
    }

    function appendModelPrefix(value, prefix) {
        if (value.indexOf("*.") === 0) {
            value = value.replace("*.", prefix);
        }
        return value;
    }

    function compareBase(options, ruleName) {
        var prefix = getModelPrefix(options.element.name);
        var other = appendModelPrefix(options.params.other, prefix);
        var element = $(options.form).find(":input[name=" + other + "]")[0];

        setValidationValues(options, ruleName, element);
    }

    var adapters = $.validator.unobtrusive.adapters;

    adapters.add("equalsto", ["other"], function (options) {
        compareBase(options, "equalsto");
    });

    adapters.add("greaterthan", ["other"], function (options) {
        compareBase(options, "greaterthan");
    });

    adapters.add("greaterthanorequalsto", ["other"], function (options) {
        compareBase(options, "greaterthanorequalsto");
    });

    adapters.add("lessthan", ["other"], function (options) {
        compareBase(options, "lessthan");
    });

    adapters.add("lessthanorequalsto", ["other"], function (options) {
        compareBase(options, "lessthanorequalsto");
    });

    adapters.add("notequalsto", ["other"], function (options) {
        compareBase(options, "notequalsto");
    });
} (jQuery));