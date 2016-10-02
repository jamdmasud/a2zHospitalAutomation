var detailList = [];

function edit_row(no)
{
 document.getElementById("edit_button"+no).style.display="none";
 document.getElementById("save_button"+no).style.display="inline";
 document.getElementById("save_button"+no).style.width="50px";
 
	
 var name=document.getElementById("name_row"+no);
 var country=document.getElementById("country_row"+no);
 var age=document.getElementById("age_row"+no);
	
 var name_data=name.innerHTML;
 var country_data=country.innerHTML;
 var age_data=age.innerHTML;
	
 name.innerHTML="<input  class='form-control' type='text' id='name_text"+no+"' value='"+name_data+"'>";
 country.innerHTML="<input class='form-control'  type='text' id='country_text"+no+"' value='"+country_data+"'>";
 age.innerHTML="<input class='form-control' type='text' id='age_text"+no+"' value='"+age_data+"'>";
 document.getElementById("name_text"+no);
}

function save_row(no)
{
 var name_val=document.getElementById("name_text"+no).value;
 var country_val=document.getElementById("country_text"+no).value;
 var age_val=document.getElementById("age_text"+no).value;

 document.getElementById("name_row"+no).innerHTML=name_val;
 document.getElementById("country_row"+no).innerHTML=country_val;
 document.getElementById("age_row"+no).innerHTML=age_val;

 document.getElementById("edit_button"+no).style.display="inline";
 document.getElementById("save_button"+no).style.display="none";
 document.getElementById("new_name").focus();
}

function delete_row(no)
{
 document.getElementById("row"+no+"").outerHTML="";
 document.getElementById("new_name").focus();
}

function add_row()
{
 var new_name=document.getElementById("new_name").value;
 var new_country=document.getElementById("new_country").value;
 var new_age=document.getElementById("new_age").value;
	if(new_name == "" || new_country == "" || new_age == ""){
		alert('Please fill up all field!');
	}else{
	 var table=document.getElementById("data_table");
	 var table_len=(table.rows.length)-1;
	 var row = table.insertRow(table_len).outerHTML="<tr id='row"+table_len+"'><td id='name_row"+table_len+"'>"+new_name+"</td><td id='country_row"+table_len+"'>"+new_country+"</td><td id='age_row"+table_len+"'>"+new_age+"</td><td><button id='edit_button"+table_len+"' value='Edit' class='btn btn-md btn-primary edit' onclick='edit_row("+table_len+")'><span class='glyphicon glyphicon-pencil' style='font-size:25px;width:50px'></span></button> <button id='save_button"+table_len+"' value='Save' class='btn btn-md btn-primary save'  style='display:none;font-size:25px;width:50px' onclick='save_row("+table_len+")'> <span class='glyphicon glyphicon-floppy-disk' style='fon-size:25px'></span></button> <button value='Delete' class='btn btn-md btn-danger delete' onclick='delete_row("+table_len+")'><span class='glyphicon glyphicon-trash' style='font-size:25px;width:50px'></span></button></td></tr>";
	    
	 document.getElementById("new_name").value="";
	 document.getElementById("new_country").value="";
	 document.getElementById("new_age").value="";
	 document.getElementById("new_name").focus();
	  
	}
}