
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("namesDropdown").addEventListener("change", function () {
        var selectedId = this.value; 
        var rolesDropdown = document.getElementById("rolesDropdown");
        rolesDropdown.innerHTML = '<option value="">Select Role...</option>';
        var nameOptions = this.options; 

        for (var i = 0; i < nameOptions.length; i++) {
            var opt = nameOptions[i];

            if (opt.value === selectedId) { 
                var empRole = opt.getAttribute('data-emp-role');
                console.log("Employee Role:", empRole); 
                var option = new Option(empRole, empRole);
                option.selected = true;
                rolesDropdown.add(option);
                break; 
            }
        }
    });
});
