const j = jQuery.noConflict();
const VALIDATE_IUPI_EMPTY_VAL = "";
const NO_DETAILS_VALUE = "no_details";
const EXTERNAL_USER_ITEM = {
    label: EXTERNAL_USER_STRING,
    value: NO_DETAILS_VALUE,
    name: "ext",
    iupi: "ext",
    bonds: null,
    uu: EXTERNAL_USER_LOCK_LABEL,
    photo: ""
};
const NO_RESULTS_ITEM = {
    label: NO_RESULTS_STRING,
    value: NO_DETAILS_VALUE
};

var iupiArray = [];

var displayAutocompleteDetails = false;

function updateAutocompleteDetails(item) {
    j("#autocomplete_details").hide();

    if(item.value == NO_DETAILS_VALUE) {
        displayAutocompleteDetails = false;
        return;
    }

    displayAutocompleteDetails = true;
    
    if(item.photo != null && item.photo != "") {
        j("#details_photo").attr("src", "data:image/png;base64, " + item.photo);
    } else {
        j("#details_photo").attr("src", "image/submit/unknown_user.jpg");
    }
    j("#details_name").text(item.name);
    j("#details_uu").text(item.uu);

    j("#details_bonds tr").remove();

    if(item.bonds.length > 0) {
        j("#details_bonds").append(
            "<tr>" +
                "<th>" + UNIT_STRING + "</th>" +
                "<th>" + BOND_STRING + "</th>" +
                "</tr>"
        );

        var index = 0;
        j.each(item.bonds, function(outerKey, outerValue) {
            j.each(outerValue, function(key, value) {
                j("#details_bonds").append("<tr>" +
                                           "<td class='ua-bonds-unit'>" +
                                           key +
                                           "</td>" +
                                           "<td id='ua-bond-" +
                                           index + "' class='ua-bonds-bond'>" +
                                           "</td>" +
                                           "</tr>"
                                          );
                
                j.each(value, function(innerKey, innerValue) {
                    j("#ua-bond-" + index).append("<ul>" +
                                                  "<li>" +
                                                  innerValue +
                                                  "</li>" +
                                                  "</ul>");
                });
            });
            ++index;
        });
    }
}

function autocompleteMousemoveHandler(e) {
    j('#autocomplete_details').hide();
    j('#autocomplete_details').css({'top':e.pageY-100,'left': e.pageX - 420});
    j("#autocomplete_details").show();
}

function lockAuthor(obj, item) {

    if(obj == null) return;

    obj.removeClass("ui-autocomplete-loading");
    
    const id = obj.attr("id");
    const lock_id = "details_" + obj.attr("id");
    const name = item.iupi != "ext" ? item.iupi : obj.val() + "|ext";

    obj.parent().attr("class", function() {
        return j(this).attr("class") + " input-group";
    });
    obj.after("<div id='" + lock_id +
              "' name='" + name  +
              "' class='input-group-addon ua-uu-tag'>" +
              "<div title='Remover autoridade'" +
              " id='remove_" + id + "'" +
              " class='glyphicon glyphicon-remove ua-uu-tag-remove'></div>" +
              item.uu +
              "</div>");

    if(item.label != EXTERNAL_USER_STRING) {
        j("#" + lock_id).mousemove(autocompleteMousemoveHandler);
        j("#" + lock_id).hover(function(e) {
            updateAutocompleteDetails(item);
        }, function(e) {
            displayAutocompleteDetails = false;
            j("#autocomplete_details").hide();
        });
        iupiArray.push(item.iupi);
    } else {
        j("#" + lock_id).hover(function(e) {
            displayAutocompleteDetails = false;
            j("#autocomplete_details").hide();
        }, function(e) {
            displayAutocompleteDetails = false;
            j("#autocomplete_details").hide();
        });
    }

    j("#remove_" + id).hover(function(e) {
        j("#" + lock_id).off('mousemove');
        displayAutocompleteDetails = false;
        j("#autocomplete_details").hide();
    }, function(e) {
        if(item.iupi != "ext") {
            j("#" + lock_id).mousemove(autocompleteMousemoveHandler);
        }
    }).click(function(e) {
        const iupi = j(this).parent().attr("name");
        
        j("[id^=ua_dados_iupi]").each(function() {
            if(j(this).val() == iupi) {
                j(this).val("");
                return false;
            }
        });

        j("[id^=ua_dados_aliasiupi]").each(function() {
            const aliasiupi = j(this).val().split("|");
            const valToCompare = aliasiupi[1] != "ext" ? aliasiupi[1] :
                  j(this).val();
            if(valToCompare == iupi) {
                j(this).val("");
                return false;
            }
        });

        iupiArray = j.grep(iupiArray, function(val) {
            return val != iupi;
        });
        
        j("#" + lock_id).remove();
        obj.autocomplete("search", obj.val());
        obj.focus();
    });

    obj.data("val", obj.val());
    enablePopover();
}

function getUnlockedAuthorFieldFromIUPI(iupi) {
    var retobj = null;
    j("[id^=ua_dados_aliasiupi]").each(function() {
        if(retobj != null) return false;
        var aliasiupi = j(this).val().split("|");
        if(aliasiupi[1] != iupi) return true;
        j("[id^=dc_contributor]").each(function() {
            var isLocked = j("#details_" + j(this).attr("id")).length;
            var content = j(this).val();
            if(!isLocked && content != "" && content == aliasiupi[0]) {
                retobj = j(this);
                return false;
            }
        });
    });
    return retobj;
}

function getUnlockedAuthorFieldFromName(name) {
    var retobj = null;
    j("[id^=dc_contributor]").each(function() {
        var isLocked = j("#details_" + j(this).attr("id")).length;
        var content = j(this).val();
        if(!isLocked && content != "" && content == name) {
            retobj = j(this);
            return false;
        }
    });
    return retobj;
}

function getNotLoadingAuthorFieldFromName(name) {
    var retobj = null;
    j("[id^=dc_contributor]").each(function() {
        var isLocked = j("#details_" + j(this).attr("id")).length;
        var isLoading = j(this).hasClass("ui-autocomplete-loading");
        var content = j(this).val();
        if(!isLocked && !isLoading && content != "" && content == name) {
            retobj = j(this);
            return false;
        }
    });
    return retobj;
}

function getUsersFromIUPIS(iupiArray) {
    j.ajax({
        url : "rcu-requests",
        type : "GET",
        data : {
            iupis : JSON.parse(JSON.stringify(iupiArray))
        },
        datatype : "json",
        success : function(data) {
            j.each(data, function(index, item) {
                var authorField = getUnlockedAuthorFieldFromIUPI(item.iupi);
                if(authorField != null) {
                    lockAuthor(authorField, item);
                }
            });

            enablePopover();
        }
    });
}


function enablePopover() {

    // if it already exists, destroy it
    disablePopover();
    
    // obtain the one with the lowest X coord
    var removeButton = null;
    var maxX = Number.MAX_SAFE_INTEGER;
    j("[id^='remove_dc_contributor_']").each(function() {
        var thisX = j(this).position().top;
        if(thisX < maxX) {
            maxX = thisX;
            removeButton = j(this);
        }
    });

    if(removeButton == null) return;

    removeButton.popover({
        html : true,
        //container : 'body',
        placement:'top',
        //content:'Clique aqui se desejar remover a autoridade que seleccionou.'
        content: POPOVER_STRING
    }).popover('show');

    j(".popover-content").hover(
        function(e) {
            const parent = j(this).parent().parent();
            parent.off('mousemove');
            displayAutocompleteDetails = false;
            j("#autocomplete_details").hide();
        },
        function(e) {
            const parent = j(this).parent().parent();
            if(!parent.attr("name").includes("ext")) {
                parent.mousemove(autocompleteMousemoveHandler);
            }
        }
    );
}

function disablePopover() {
    j(".popover").closest("div.popover").popover("destroy");
}

// workaround to guarantee that, while using Chrome / Chromium, there's no
// discrepancy in the author fields and corresponding iupi and aliasiupi fields
// (3 author fields and only 2 iupi fields, for example)
function ensureEnoughIupiFields() {
    const authorCount = j("[id^=dc_contributor]").length;
    const createIupi = authorCount - j("[id^=ua_dados_iupi]").length;
    const createAliasIupi =
          authorCount - j("[id^=ua_dados_aliasiupi]").length;
    const createMoreIupiFields = createIupi > 0;
    const createMoreAliasIupiFields = createAliasIupi > 0;

    if(createMoreIupiFields || createMoreAliasIupiFields) {
        // this is done because dspace removes empty ('') fields when adding
        // more iupi / aliasiupi fields. if this is not done, this script
        // gets stuck in an infinite loop while trying to ensure that there
        // are enough fields for the number of authors
        j("[id^='ua_dados_iupi']").each(function() {
            if(j(this).val() == "") {
                j(this).val("empty");
            }
        });
        j("[id^='ua_dados_aliasiupi']").each(function() {
            if(j(this).val() == "") {
                j(this).val("empty");
            }
        });

        if(createMoreIupiFields) {
            j("button[name='submit_ua_dados_iupi_add']").click();
        }
        if(createMoreAliasIupiFields) {
            j("button[name='submit_ua_dados_aliasiupi_add']").click();
        }
    }
}

function monitorInputs() {
    j(document).on("focusin", "[id^=dc_contributor]", function() {
        current = j(this).val().trim().replace(/\s\s+/g, ' ');
        j(this).removeClass("ua-author-error");
        j(this).data("val", current);
        j(this).autocomplete("search", current);
    }).on("change", "[id^=dc_contributor]", function() {

        if(j("#details_" + j(this).attr("id")).length == 0) return true;
        
        var prev = j(this).data("val");
        var current = j(this).val();

        current = current.trim().replace(/\s\s+/g, ' ');

        if(current != prev) {
            j("[id^=ua_dados_aliasiupi]").each(function() {
                var aliasiupi = j(this).val().split("|");
                var cleanalias = aliasiupi[0].trim().replace(/\s\s+/g, ' ');
                if(cleanalias != prev) return true;
                j(this).val(current + "|" + aliasiupi[1]);
                return false;
            });
        }
    });
}

function restoreData() {
    j("[id^=ua_dados_aliasiupi]").each(function() {

        if(j(this).val() == "") return true;
        
        var aliasiupi = j(this).val().split("|");

        var authorField = getNotLoadingAuthorFieldFromName(aliasiupi[0]);
        if(authorField != null) {
            authorField.addClass("ui-autocomplete-loading");
        }
        
        if(aliasiupi[1] == "ext") {
            authorField = getUnlockedAuthorFieldFromName(aliasiupi[0]);
            EXTERNAL_USER_ITEM.name = aliasiupi[0];
            if(authorField != null) lockAuthor(authorField, EXTERNAL_USER_ITEM);
        }
    });
    
    j("[id^=ua_dados_iupi]").each(function() {
        var temp = j(this).val();
        if(temp != "") iupiArray.push(temp);
    });

    if(iupiArray.length > 0) {
        getUsersFromIUPIS(iupiArray);
    } else {
        enablePopover();
    }
}

function validateExistingIupi() {
    j("[id^=ua_dados_aliasiupi]").each(function() {
        var aliasiupielem = j(this);
        var aliasiupi = j(this).val().split("|");
        var name = aliasiupi[0];
        var iupi = aliasiupi[1];
        var exists = false;
        
        j("[id^=dc_contributor]").each(function() {
            const value = j(this).val().strip();
            if(value != "" && value == name) {
                exists = true;
                return false;
            }
        });

        if(!exists) {
            j("[id^=ua_dados_iupi").each(function() {
                if(j(this).val() == iupi) {
                    j(this).val(VALIDATE_IUPI_EMPTY_VAL);
                }
            });

            if(iupi != "ext") {
                aliasiupielem.val(VALIDATE_IUPI_EMPTY_VAL);
            }
        }        
    });
    j("[id^=ua_dados_iupi]").each(function() {
        var iupi = j(this).val();
        var exists = false;
        j("[id^=ua_dados_aliasiupi]").each(function() {
            var iupi2 = j(this).val().split("|")[1];
            if(iupi == iupi2) {
                exists = true;
                return false;
            }
        });

        if(!exists) j(this).val(VALIDATE_IUPI_EMPTY_VAL);
    });
}

function setHints() {
    j("[id^=dc_contributor]").each(function() {
        if(j(this).val() == "") {
            j(this).attr("placeholder", SEARCH_HINT_STRING);
        }
    });
}

function bootstrapAlertOnAuthor(id, message) {

    if(j("#" + id).length) return;
    
    const alertDiv = j("<div>").attr({
        id: id,
        class: "alert alert-danger"
    }).text(message);
    const parentDiv =
          j("#dc_contributor").parent().parent().parent().parent()

    parentDiv.before(alertDiv);
}

function nextClickHandler(e) {
    var noAuth = false;
    var noName = false;
    j("input[id^='dc_contributor']").each(function() {
        const isLocked = j("#details_" + j(this).attr("id")).length;

        if(j(this).val() != "" && !isLocked) {
            j(this).addClass("ua-author-error");
            noAuth = true;
        }

        if(j(this).val() == "" && isLocked) {
            j(this).addClass("ua-author-error");
            noName = true;
        }
    });

    if(noAuth == true || noName == true) {
        
        if(noAuth) bootstrapAlertOnAuthor("no_auth_alert", NO_AUTH_STRING);
        else j("#no_auth_alert").remove();
        
        if(noName) bootstrapAlertOnAuthor("no_name_alert", NO_NAME_STRING);
        else j("#no_name_alert").remove();
        
        e.preventDefault();
        alert(AUTHOR_ERROR_STRING);
    }
}

j("#ua_dados_iupi").parent().parent().parent().parent()
    .attr("style", "display:none;");
j("#ua_dados_aliasiupi").parent().parent().parent().parent()
    .attr("style", "display:none;");

j("input[name='submit_next']").click(nextClickHandler);
j("input[name^='submit_jump']").click(nextClickHandler);

setHints();
validateExistingIupi();
ensureEnoughIupiFields();
monitorInputs();
restoreData();

j("[id^=dc_contributor]").each(function() {
    var authorElem = j(this);
    j(this).autocomplete({
        source: function(request, response) {
            j.ajax({
                url : "rcu-requests",
                type : "GET",
                data : {
                    page : 1,
                    usersPerPage : 20,
                    term : authorElem.val()
                },
                datatype : "json",
                error : function() {
                    if(!authorElem.val().endsWith("@ua.pt")) {
                        response([EXTERNAL_USER_ITEM]);
                    } else {
                        response([NO_RESULTS_ITEM]);
                    }
                },
                complete : function() {
                    j(authorElem).removeClass("ui-autocomplete-loading");
                },
                success : function(data) {
                    var ret = j.map(data, function(item) {
                        return {
                            label: item.name,
                            value: item.name,
                            name: item.name,
                            iupi: item.iupi,
                            bonds: item.bonds,
                            uu: item.uu,
                            photo: item.photo
                        };
                    });
                    if(!authorElem.val().endsWith("@ua.pt")) {
                        ret.unshift(EXTERNAL_USER_ITEM);
                    }
                    response(ret);
                }
            });            
        },
        minLength: 3,
        delay: 1000,
        create: function(e, ui) {
            j(this).data("ui-autocomplete")._renderMenu = function(ul, items) {
                j(ul).unbind("scroll");
                var self = this;
     //           self._renderItemData(ul, EXTERNAL_USER_ITEM);
                j.each(items, function(index, item) {
                    self._renderItemData(ul, item);
                });
                this._scrollMenu(ul, items);
                ul.mousemove(function(e) {
                    j("#autocomplete_details").css(
                        {"top":e.pageY-100,"left":e.pageX + 20});
                    if(displayAutocompleteDetails) j("#autocomplete_details")
                        .show();
                });
            };
            j(this).data("ui-autocomplete")._scrollMenu = function(ul, items) {
                var self = this;
                var searchOngoing = false;
                window.pageIndex = 2;
                j(ul).scroll(function() {
                    displayAutocompleteDetails = false;
                    j("#autocomplete_details").hide();
                    var div = j(this);
                    var scrollBarAtBottom =
                        (div[0].scrollHeight - div.scrollTop()) ==
                        div.innerHeight();

                    if(scrollBarAtBottom && !searchOngoing) {
                        j.ajax({
                            url: "rcu-requests",
                            type: "GET",
                            data: {
                                page: window.pageIndex,
                                usersPerPage: 20,
                                term: j(authorElem).val()
                            },
                            dataType: "json",
                            beforeSend: function() {
                                j(authorElem)
                                    .addClass("ui-autocomplete-loading");
                                searchOngoing = true;
                            },
                            complete: function() {
                                j(authorElem)
                                    .removeClass("ui-autocomplete-loading");
                                searchOngoing = false;
                                ++window.pageIndex;
                            },
                            success: function(data) {
                                var convData = j.map(data, function(item) {
                                    return {
                                        label: item.name,
                                        value: item.name,
                                        name: item.name,
                                        iupi: item.iupi,
                                        bonds: item.bonds,
                                        uu: item.uu,
                                        photo: item.photo
                                    };
                                });

                                //append item to ul
                                j.each(convData, function(index, item) {
                                    self._renderItemData(ul, item);
                                });

                                //refresh menu
                                self.menu.refresh();
                                // size and position menu
                                ul.show();
                                self._resizeMenu();
                                ul.position(j.extend({
                                    of: self.element
                                }, self.options.position));
                                if (self.options.autoFocus) {
                                    self.menu.next(new j.Event("mouseover"));
                                }

                                enablePopover();
                            }
                        }
                              );
                    }
                });
            };
            j(this).data("ui-autocomplete")._renderItem = function(ul, item) {
                if(item.value != NO_DETAILS_VALUE) {
                    var photo;
                    if(item.photo != null && item.photo != "") {
                        photo = "data:image/png;base64, " + item.photo;
                    } else {
                        photo = "image/submit/unknown_user.jpg";
                    }

                    if(j.inArray(item.iupi, iupiArray) > -1) {
                        return j("<li " +
                                 "class='ua-item-disabled ui-state-disabled'>" +
                                 "</li>")
                            .data("item.autocomplete", item)
                            .append("<img class='ua-autocomplete-photo' " +
                                    "src='" + photo  + "' />"  +
                                    item.label + "   (" + item.uu + ")")
                            .appendTo(ul);
                    } else {
                        return j("<li></li>")
                            .data("item.autocomplete", item)
                            .append("<a>" +
                                    "<img class='ua-autocomplete-photo' " +
                                    "src='" + photo  + "' />"  +
                                    item.label + "   (" + item.uu + ")" +
                                    "</a>")
                            .appendTo(ul);
                    }
                } else if(item.value == NO_DETAILS_VALUE) {
                    return j("<li></li>")
                        .data("item.autocomplete", item)
                        .append("<a class='ua-external-user-string'>" +
                                item.label +
                                "</a>")
                        .appendTo(ul);
                }
            };
        },
        search: function(e, ui) {
            if(j("#details_" + j(this).attr("id")).length) {
                e.preventDefault();
            } else {
                disablePopover();
            }
            j(this).data("ui-autocomplete").menu.element.scrollTop(0);
        },
        complete: function() {
            j(this).removeClass("ui-autocomplete-loading");
        },
        focus: function(e, ui) {
            e.preventDefault();
            updateAutocompleteDetails(ui.item);
        },
        close: function(e, ui) {
            displayAutocompleteDetails = false;
            j("#autocomplete_details").hide();
            enablePopover();
        },
        select: function(e, ui) {
            e.preventDefault();

            if(ui.item.label == NO_RESULTS_STRING) return;
            
            var id = j(this).attr("id");
            var name = j(this).val();

            j("[id^=ua_dados_iupi]").each(function() {
                if(!j(this).val() && ui.item.iupi != "ext") {
                    j(this).val(ui.item.iupi);
                    return false;
                }
            });

            j("[id^=ua_dados_aliasiupi]").each(function() {
                if(!j(this).val()) {
                    j(this).val(name + "|" + ui.item.iupi);
                    return false;
                }
            });

            lockAuthor(j(this), ui.item);
        }
    }).mouseout(function() {
        displayAutocompleteDetails = false;
        j("#autocomplete_details").hide();
    });
});
