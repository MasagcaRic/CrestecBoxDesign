document.addEventListener("DOMContentLoaded", function () {
    console.log("JavaScript Loaded!");
    // Ensure checkboxes remain independent
    const checkboxes = document.querySelectorAll(".checkbox-group input[type='checkbox']");

    checkboxes.forEach(chk => {
        chk.addEventListener("change", function () {
            // No logic to uncheck other checkboxes, allowing multiple selections
        });
    });

    // Material Type Dropdown Logic
    let materialDropdown = document.getElementById("<%= ddl_material_type.ClientID %>");
    let otherMaterialInput = document.getElementById("<%= txt_other_material.ClientID %>");

    function toggleOtherMaterial() {
        otherMaterialInput.disabled = materialDropdown.value !== "Other";
        if (otherMaterialInput.disabled) {
            otherMaterialInput.value = "";
        }
    }

    materialDropdown.addEventListener("change", toggleOtherMaterial);
    toggleOtherMaterial(); // Initialize on page load
});

// Prevent users from using the back button after login/logout
Page.ClientScript.RegisterStartupScript(this.GetType(), "DisableBackButton", "window.history.forward();", true);

document.addEventListener("DOMContentLoaded", function () {
    let materialDropdown = document.getElementById("<%= ddl_material_type.ClientID %>");
    let otherMaterialInput = document.getElementById("<%= txt_other_material.ClientID %>");

    if (materialDropdown && otherMaterialInput) {
        materialDropdown.addEventListener("change", function () {
            otherMaterialInput.disabled = materialDropdown.value !== "Other";
            if (otherMaterialInput.disabled) {
                otherMaterialInput.value = "";
            }
        });
    }
});