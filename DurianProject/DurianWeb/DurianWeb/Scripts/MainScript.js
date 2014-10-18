// JScript File

var dayStr = new Array("อาทิตย์","จันทร์","อังคาร","พุธ","พฤหัสบดี","ศุกร์","เสาร์");
var dayStrEng = new Array("SUN","MON","TUE","WED","THU","FRI","SAT");
var monthThaiStr =  new Array("มกราคม","กุมภาพันธ์","มีนาคม","เมษายน","พฤษภาคม","มิถุนายน","กรกฎาคม","สิงหาคม","กันยายน","ตุลาคม","พฤศจิกายน","ธันวาคม");
var monthEngStr =  new Array("January","February","March","April","May","June","July","August","September","October","November","December");
var monthEng =  new Array("JAN","FEB","MAR","APR","MAY","JUN","JUL","AUG","SEP","OCT","NOV","DEC");
function GetThaiDateTime(dateObj) {
    var ResStr = "";
    //alert(dateObj);
    if (dateObj != null && dateObj.toString() != "")
        ResStr = dateObj.getDate() + " " + monthThaiStr[dateObj.getMonth()] + " " + (dateObj.getFullYear() > 3000 ? dateObj.getFullYear() + 543 : dateObj.getFullYear());
    if (dateObj != null)
        ResStr += " " + (dateObj.getHours().toString().length == 1 ? "0" + dateObj.getHours().toString() : dateObj.getHours().toString());
    ResStr += ":" + (dateObj.getMinutes().toString().length == 1 ? "0" + dateObj.getMinutes().toString() : dateObj.getMinutes().toString());
    ResStr += ":" + (dateObj.getSeconds().toString().length == 1 ? "0" + dateObj.getSeconds().toString() : dateObj.getSeconds().toString());
    return ResStr;
}
function GetThaiDate(dateObj){
      var ResStr = "";
      //alert(dateObj);
      if(dateObj != null && dateObj.toString() != "")
            ResStr = dateObj.getDate() + " " + monthThaiStr[dateObj.getMonth()] + " " + (dateObj.getFullYear() > 3000?dateObj.getFullYear() + 543: dateObj.getFullYear());
      return ResStr;
}
function GetEngDate(dateObj){
      var ResStr = "";
      //alert(dateObj);
      if(dateObj != null && dateObj.toString() != "")
            ResStr = dateObj.getDate() + " " + monthEngStr[dateObj.getMonth()] + " " + (dateObj.getFullYear() > 3000?dateObj.getFullYear() + 543: dateObj.getFullYear());
      return ResStr;
}
function GetEngDateFull(dateObj){
      var ResStr = "";
      //alert(dateObj);
      if(dateObj != null && dateObj.toString() != "")
            ResStr =  dayStrEng[dateObj.getDay()] + " " + dateObj.getDate() + " " + monthEng[dateObj.getMonth()] + " " + (dateObj.getFullYear() > 3000?dateObj.getFullYear() + 543: dateObj.getFullYear());
      return ResStr;
}
function GetNormalDate(dateObj){
     var ResStr = "";
      //alert(dateObj);
      if(dateObj != null && dateObj.toString() != "")
            ResStr = dateObj.getDate() + "/" + (dateObj.getMonth() + 1) + "/" + (543 + dateObj.getFullYear());
      return ResStr;
}
function GetNormalDateEng(dateObj){
     var ResStr = "";
      //alert(dateObj);
      if(dateObj != null && dateObj.toString() != "")
            ResStr = dateObj.getDate() + "/" + (dateObj.getMonth() + 1) + "/" + (dateObj.getFullYear());
      return ResStr;
}
function GetSqlStrDate(dateObj){
	  var ResStr = "";
	  if(dateObj != null)
            ResStr = dateObj.getFullYear() + "-" + (dateObj.getMonth() + 1) + "-" + dateObj.getDate();
      return ResStr;
}
function GetTimeFormat(dateObj){
      var ResStr = "";
      if(dateObj != null)
                  ResStr = (dateObj.getHours().toString().length == 1? "0" + dateObj.getHours().toString():dateObj.getHours().toString());
                   ResStr += ":" + (dateObj.getMinutes().toString().length == 1? "0" + dateObj.getMinutes().toString():dateObj.getMinutes().toString());
                   ResStr += ":" + (dateObj.getSeconds().toString().length == 1? "0" + dateObj.getSeconds().toString():dateObj.getSeconds().toString());
       return ResStr;
}

function SetThaiToDate(dateStr){
     var ResStr = "";
     var StrArr = dateStr.split(" ");
     var i;
     for(i = 0;i < monthThaiStr.length;i++){
        if(monthThaiStr[i] == StrArr[1]) break;
     }
     ResStr = i + "-" + StrArr[0] + "-" + (parseInt(StrArr[2]) - 543);
     return new Date(ResStr);
}
function SetEngToDate(dateStr){
     var ResStr = "";
     var StrArr = dateStr.split(" ");
     var i;
     for(i = 0;i < monthEngStr.length;i++){
        if(monthEngStr[i] == StrArr[1]) break;
     }
     ResStr = i + "-" + StrArr[0] + "-" + (parseInt(StrArr[2]) - 543);
     return new Date(ResStr);
}

function SetValueToClientTag(TagID,Values){
      document.getElementById(TagID).innerHTML = Values;
     // document.getElementById("ItemID").value = Values;
}

function DoPostBack(Command,Values,ConfText){
    if(ConfText == ""){
        document.getElementById("Command").value = Command;
        document.getElementById("ItemID").value = Values;
        document.forms(0).submit();
    }else{
        if(confirm(ConfText)){
            document.getElementById("Command").value = Command;
            document.getElementById("ItemID").value = Values;
            document.forms(0).submit();
        }
    }
}
function SetDataForPostBack(TagID,TValues,PValues){
      document.getElementById(TagID).innerHTML = TValues;
      document.getElementById("ItemID").value = PValues;
}

function SetValueToServerTag(TagID,Values){
      var AllElements = document.forms(0).elements;
      var i = 0;
      for(i=0;i<AllElements.length;i++){
            if(AllElements[i].id.indexOf(TagID) != -1){
                  AllElements[i].value = Values;
                  break;
            }
      }
      document.getElementById ("ItemID").value = Values;
}
function CurrencyFormat(values){
 //     values = values.replace(/,/g,"");
//      var Nagative
//      
      var Nagative = false;
    if(values.toString().indexOf("-") != -1){
        Nagative = true;
        //alert(Math.abs(parseFloat(values)));
        values = Math.abs(values);
    }
    if(!isNaN(values)){
          values = values.toString().split('.');
          if(values[0] == ""){values= "0";}
          if(values.length == 1){values += ".00" ; values = values.split('.'); }
          var Result="", a = values[0].length % 3;
          if(a != 0){
	        Result = values[0].substring(0, a ) + ",";
	        values[0] = values[0].substring(a, values[0].length );
          }
          for(var i = 0 ;i < values[0].length - 1 ; i += 3 ){
	        Result += values[0].substring(i ,i + 3) + ",";
          }
          if(Nagative)
            return "-" + Result.substring(0,Result.length -1) + "." + values[1].substring(0,2);
          else
            return Result.substring(0,Result.length -1) + "." + values[1].substring(0,2);
          
      }else{
          return "0.00";
      }
}

function OpenWindow(WPath, WName, WWidth, WHeight){
    var Pwidth = WWidth;
    var Pheight = WHeight;
    var PName = WName;
    var Path = WPath;
    var _Resize = "no";
    var _Titlebar = "no";
    var _Status = "no";
    var _Location = "no";
    var _Scrollbar = "no";
    this.open = function(){
            var DisplayX = (screen.width / 2) - (Pwidth / 2);
            var DisplayY = (screen.height / 2) - (Pheight / 2);
            var Parameter = "width=" + Pwidth.toString() + ",height=" + Pheight.toString() + ",left=" + DisplayX + ",top=" + DisplayY + ",resize=" + _Resize + ",titlebar=" + _Titlebar + ",status=yes,location=" + _Location + ",scrollbar=" + _Scrollbar ;
            //alert(Parameter);
            window.open(Path + "?" + this.createParameters(), PName, Parameter);
    }
    this.openfull = function(){
            window.open(Path + "?" + this.createParameters(), PName);
    }
    
    this.Resize = function(Value){_Resize = Value;}
    this.Titlebar = function(Value){_Titlebar= Value;}
    this.Status = function(Value){_Status= Value;}
    this.Location = function(Value){_Location= Value;}
    this.Scrollbar = function(Value){_Scrollbar= Value;}
    
    this.Param = new Array();
    this.addParam = function(name, value){
	    this.Param.push(name + "=" + encodeURI(value));
    }

    this.createParameters = function(){
        var q = "";
        if(this.Param.length > 0){
            for(i=0; i<this.Param.length; i++){
                q += this.Param[i] + "&";
            }
            q = q.substr(0, q.length-1);
        }
        return q;
    }
}


//ฟังก์ชันเปลี่ยนข้อความ
	function StrReplace(OrgStr,Str1,Str2){
	    var ResStr = OrgStr;
	    //alert(ResStr);
	    while(ResStr.indexOf(Str1) != -1){
	        ResStr = ResStr.replace(Str1,Str2);
	    }
	    //alert(ResStr);
	    return ResStr;
	}
//ปัดเศษ valueค่าที่ต้องการปัด digit ต้องการปัดเศษหลักไหน

	function Rounding(value,digit){
        var Res = value;
        var Mod = Res % digit;
        if(Mod >= (digit / 2)){
            Res = Res - Mod;
            Res += digit;
        }else{
            Res = Res - Mod;
        }
        return Res;
    }
 function oncalEndDateChange(calendar) {
        var fromDate =  calStartDate.GetSelectedDate();
        var toDate = calEndDate.GetSelectedDate();
         if (toDate < new Date()){
            alert("ไม่สามารถเลือกวันที่ผ่านมาแล้ว");
            fromDate = new Date();
            fromDate.setDate(fromDate.getDate() + 1);
            calStartDate.SetSelectedDate(fromDate);
            picStartDate.SetSelectedDate(fromDate);
            calEndDate.SetSelectedDate(fromDate);
            picEndDate.SetSelectedDate(fromDate);
        }else{
            if (toDate < fromDate){
                alert('เลือกวันใหม่เนื่องจากวันที่เริ่ม มากกว่าวันที่ จบ');
                calEndDate.SetSelectedDate(fromDate);
                picEndDate.SetSelectedDate(fromDate);
            }else{  
                              
                calEndDate.SetSelectedDate(toDate);
                picEndDate.SetSelectedDate(toDate);
            }   
        }
    }