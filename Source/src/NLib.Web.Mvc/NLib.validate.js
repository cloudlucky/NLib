// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NLib.validate.js" company=".">
//   Copyright (c) Cloudlucky. All rights reserved.
//   http://www.cloudlucky.com
//   This code is licensed under the Microsoft Public License (Ms-PL)
//   See http://www.microsoft.com/opensource/licenses.mspx#Ms-PL.
// </copyright>
// <note>
//   Depends on jQuery Validation Plugin 1.8.0 (jquery.validate.js)
// </note
// --------------------------------------------------------------------------------------------------------------------


(function ($) {
    function getTargetCompareBase(value, element, param, ruleName) {
        return $(param).unbind(".validate-" + ruleName).bind("blur.validate-" + ruleName, function () {
            $(element).valid();
        });
    }

    $.validator.addMethod("equalsto", function (value, element, param) {
        var target = getTargetCompareBase(value, element, param, "equalsto");

        return value == target.val();
    });

    $.validator.addMethod("greaterthan", function (value, element, param) {
        var target = getTargetCompareBase(value, element, param, "greaterthan");

        return value > target.val();
    });

    $.validator.addMethod("greaterthanorequalsto", function (value, element, param) {
        var target = getTargetCompareBase(value, element, param, "greaterthanorequalsto");

        return value >= target.val();
    });

    $.validator.addMethod("lessthan", function (value, element, param) {
        var target = getTargetCompareBase(value, element, param, "lessthan");

        return value < target.val();
    });

    $.validator.addMethod("lessthanorequalsto", function (value, element, param) {
        var target = getTargetCompareBase(value, element, param, "lessthanorequalsto");

        return value <= target.val();
    });

    $.validator.addMethod("notequalsto", function (value, element, param) {
        var target = getTargetCompareBase(value, element, param, "notequalsto");

        return value != target.val();
    });

})(jQuery);