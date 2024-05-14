function Select2Helper() {
    return {
        init: function (option) {
            var $takeCount = option.TakeCount == null || option.TakeCount == undefined ? 10 : option.TakeCount;
            var $key = option.Key == null || option.Key == undefined ? 'Id' : option.Key;
            var $queryColumn = option.QueryColumn;
            var $tableName = option.TableName;
            var $orderColumn = option.OrderColumn == null || option.OrderColumn == undefined ? $queryColumn : option.OrderColumn;
            var $minimumInputLength = option.MinimumInputLength == null || option.MinimumInputLength == undefined ? 3 : option.MinimumInputLength;

            $('#' + option.Id).select2({
                placeholder: "Arama Yap",
                allowClear: true,
                language: {
                    // You can find all of the options in the language files provided in the
                    // build. They all must be functions that return the string that should be
                    // displayed.
                    inputTooShort: function (args) {
                        var remainingChars = args.minimum - args.input.length;

                        var message = 'Lütfen ' + remainingChars + ' veya daha fazla karakter girin.';

                        return message;
                    },
                    searching: function () {
                        return 'Aranıyor…';
                    },
                    noResults: function () {
                        return 'Hiçbirşey Bulunamadı';
                    },
                    maximumSelected: function (args) {
                        var message = 'En fazla ' + args.maximum + ' seçim yapabilirsiniz.';

                        if (args.maximum >= 2 && args.maximum <= 4) {
                            message += '';
                        } else if (args.maximum >= 5) {
                            message += '';
                        }

                        return message;
                    },
                    loadingMore: function () {
                        return 'Daha fazla kaynak yükleniyor..';
                    },
                    errorLoading: function () {
                        return 'Sonuç yüklenemedi.';
                    },
                    inputTooLong: function (args) {
                        var overChars = args.input.length - args.maximum;
                        var message = 'Lütfen ' + overChars + ' karakteri kaldırın.';
                        if (overChars >= 2 && overChars <= 4) {
                            message += '';
                        } else if (overChars >= 5) {
                            message += '';
                        }
                        return message;
                    }
                },
                ajax: {
                    url: option.Url,
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        //var $skipCount = ((query.page - 1) * option.TakeCount);
                        //var $request = {
                        //    SkipCount: $skipCount,
                        //    TableName: $tableName,
                        //    TakeCount: $takeCount,
                        //    OrderBy: 'asc',
                        //    OrderColumn: $orderColumn,
                        //    FilterParameter: []
                        //};

                        //$request.FilterParameter.push({
                        //    Operator: 'Contains',
                        //    PropertyName: 'text',
                        //    Value: params.term
                        //});

                        return {
                            q: params.term, // search term
                            page: params.page
                        };
                    },
                    processResults: function (data, params) {
                        // parse the results into the format expected by Select2
                        // since we are using custom formatting functions we do not need to
                        // alter the remote JSON data, except to indicate that infinite
                        // scrolling can be used
                        params.page = params.page || 1;
                        var results = [];
                        $.each(data, function (index, account) {
                            results.push({
                                id: account.Id,
                                text: account.Name
                            });
                        });

                        return {

                            results: results,

                            pagination: {
                                more: (params.page * 30) < data.total_count
                            }
                        };
                    },
                    cache: false
                },
                escapeMarkup: function (markup) {
                    return markup;
                }, // let our custom formatter work
                minimumInputLength: 1,
                templateResult: function (i) {
                    if (i.loading) return 'Aranıyor..';
                    return '<div>' + i.text + '</div>';
                }, // omitted for brevity, see the source of this page
                templateSelection: function (i) {
                    return '<div>' + i.text + '</div>';
                } // omitted for brevity, see the source of this page
            });

            var $select2Data = [];
            if (option.InitData != null && option.InitData != undefined) {
                if (Object.prototype.toString.call(option.InitData) === '[object Array]') {
                    $.each(option.InitData, function (i, item) {
                        $select2Data.push(new Option(item.Name, item.Id, true, true));
                    });
                    $('#' + option.Id).append($select2Data).trigger('change');
                }
                else {
                    $select2Data.push(new Option(option.InitData.Name, option.InitData.Id, true, true));
                    $('#' + option.Id).append($select2Data).trigger('change');
                }
            }
        }
    };
};

var Input = {
    SelectLoad: function (option) {
        if (option.data == null) return;
        var $ddlHtml = '';
        $.each(option.data, function (i, item) {
            if (option.additionalData != null && option.additionalData != undefined) {
                $ddlHtml += '<option value="' + item[option.value] + '" data-' + String(option.additionalData).toLowerCase() + '="' + item[option.additionalData] + '">' + item[option.text] + '</option>';
            } else {
                $ddlHtml += '<option value="' + item[option.value] + '">' + item[option.text] + '</option>';
            };
        });
        $('#' + option.Id).empty();
        var $firstOptionText = (option.firstOptionText != null || option.firstOptionText != undefined) ? option.firstOptionText : "";
        if ($firstOptionText != "" && ($('#' + option.Id).hasClass('select2') || $('#' + option.Id).hasClass('select2me'))) {
            $('#' + option.Id).data('placeholder', $firstOptionText);
        };
        if ($firstOptionText != "") {
            $('#' + option.Id).append('<option value="null">' + $firstOptionText + '</option>');
        };
        $('#' + option.Id).append($ddlHtml);
        $('#' + option.Id).select2({
            width: '100%'
        });

        if (option.selectedValue != null) {
            $('#' + option.Id).val(option.selectedValue); // Select the option with a value of '1'
            $('#' + option.Id).trigger('change');
        }
    }
};

var Ajax = {
    Post: function (url, data, successEvent, showLoading) {
        try {
            showLoading = (typeof showLoading == 'undefined' || showLoading == null) ? true : Boolean(showLoading);

            $.ajax({
                type: "POST",
                url: url,
                data: data,
                cache: false,
                dataType: "json",
                beforeSend: function () {
                    if (showLoading) {
                        KTApp.blockPage({
                            overlayColor: '#000000',
                            state: 'danger',
                            message: 'Please Waiting....'
                        });
                    }
                },
                success: function (result) {

                    if (result != null && result.Failed != null && result.Failed == true) {
                        ShowAlertError('Warning', result.Message);

                        return;
                    };
                    successEvent(result);
                },
                error: function (result) {

                },
                complete: function () {

                    if (showLoading) $.unblockUI();
                }
            });
        } catch (e) {
        };
    },
    PostFile: function (url, data, successEvent, showLoading) {
        try {
            showLoading = (typeof showLoading == 'undefined' || showLoading == null) ? true : Boolean(showLoading);
            $.ajax({
                type: "POST",
                url: url,
                data: data,
                cache: false,
                contentType: false,
                processData: false,
                beforeSend: function () {
                    if (showLoading) {
                        KTApp.blockPage({
                            overlayColor: '#000000',
                            state: 'danger',
                            message: 'Please Waiting....'
                        });
                    }
                },
                success: function (result) {
                    if (result != null && result.Failed != null && result.Failed == true) {
                        ShowAlertError('Warning', result.Message);

                        return;
                    };
                    successEvent(result);
                },
                error: function (result) {

                },
                complete: function () {

                    if (showLoading) $.unblockUI();
                }
            });
        } catch (e) {
        };
    },
    JsonPost: function (url, data, successEvent, showLoading) {
        try {

            showLoading = (typeof showLoading == 'undefined' || showLoading == null) ? true : Boolean(showLoading);
            $.ajax({
                type: "POST",
                url: url,
                data: JSON.stringify(data),
                cache: false,
                contentType: "application/json",
                dataType: "json",
                beforeSend: function () {
                    if (showLoading) {
                        KTApp.blockPage({
                            overlayColor: '#000000',
                            state: 'danger',
                            message: 'Please Waiting....'
                        });
                    }
                },
                success: function (result) {

                    if (result != null && result.Failed != null && result.Failed == true) {
                        ShowAlertError('Warning', result.Message);

                        return;
                    };
                    successEvent(result);
                },
                error: function (result) {

                },
                complete: function () {
                    if (showLoading) $.unblockUI();
                }
            });
        } catch (e) {
        };
    },
    Get: function (url, data, successEvent, showLoading) {
        try {
            showLoading = (typeof showLoading == 'undefined' || showLoading == null) ? true : Boolean(showLoading);
            $.ajax({
                type: "GET",
                url: url,
                data: data,
                cache: false,
                dataType: "json",
                beforeSend: function () {
                    if (showLoading) {
                        KTApp.blockPage({
                            overlayColor: '#000000',
                            state: 'danger',
                            message: 'Please Waiting....'
                        });
                    }
                },
                success: function (result) {

                    if (result != null && result.Failed != null && result.Failed == true) {
                        ShowAlertError('Warning', result.Message);

                        return;
                    };
                    successEvent(result);
                },
                error: function (result) {

                },
                complete: function () {
                    if (showLoading) $.unblockUI();
                }
            });
        } catch (e) {

        };
    },
    JsonGet: function (url, data, successEvent, showLoading) {
        try {
            showLoading = (typeof showLoading == 'undefined' || showLoading == null) ? true : Boolean(showLoading);
            $.ajax({
                type: "GET",
                url: url,
                data: JSON.stringify(data),
                cache: false,
                contentType: "application/json",
                dataType: "json",
                beforeSend: function () {
                    if (showLoading) {
                        KTApp.blockPage({
                            overlayColor: '#000000',
                            state: 'danger',
                            message: 'Please Waiting....'
                        });
                    }
                },
                success: function (result) {
                    if (result != null && result.Failed != null && result.Failed == true) {
                        ShowAlertError('Warning', result.Message);

                        return;
                    };
                    successEvent(result);
                },
                error: function (result) {

                },
                complete: function () {
                    if (showLoading) $.unblockUI();
                }
            });
        } catch (e) {

        };
    }
};
