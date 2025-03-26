function confirmLogout() {
    return confirm("Are you sure you want to log out?");
}
ClientScript.RegisterStartupScript(this.GetType(), "DisableBackButton", "window.history.forward();", true);

const requestData = [
    {
        id: "DRF-2024-001",
        dateSubmitted: "2024-02-25",
        type: "IDFreq",
        typeName: "IDF REQUEST",
        customer: "ABC Electronics",
        partCode: "ABC-12345-X",
        dateNeeded: "2024-03-15",
        status: "pending",
        qty: 100,
        salesInCharge: "John Doe",
        salesJapanDesk: "Tanaka Hiroshi",
        salesQaLtc: "Jane Smith",
        salesQaLisp3: "Robert Johnson",
        customerContact: "Maria Garcia",
        printType: "SILKprint",
        printTypeName: "SILK SCREEN",
        otherPrint: "",
        itemDescription: "Packaging for electronic components",
        materialType: "CORRUGATED",
        otherMaterial: "",
        sizeDimension: "OuterDimension",
        tolerance: "±0.5mm",
        printQty: 2,
        printTolerance: "±1mm",
        testRequirements: [
            { name: "BCT", value: "7.5kgf" },
            { name: "ECT", value: "4.5kgf" }
        ],
        otherTests: [
            { name: "ROHS 1", value: "Compliant" }
        ],
        projectNature: ["New Item", "Customer Supplied Drawing"],
        revisionNumber: "",
        productDescription: "Packaging must be resistant to moisture and suitable for long-term storage. Product will be shipped internationally so packaging must be durable for extended transit periods.",
        illustrationNotes: "Logo must be centered on the top panel with company colors maintained accurately.",
        uploadedFiles: ["product_drawing.pdf", "logo_specifications.ai"],
        requestedBy: "Maria Santos",
        notedBy: "James Wilson"
    },
    {
        id: "DRF-2024-002",
        dateSubmitted: "2024-02-26",
        type: "FDFreq",
        typeName: "FDF REQUEST",
        customer: "Tech Solutions Co.",
        partCode: "TS-789-Z",
        dateNeeded: "2024-03-20",
        status: "approved",
        qty: 500,
        salesInCharge: "Emily Chen",
        salesJapanDesk: "Yamamoto Kenji",
        salesQaLtc: "Michael Brown",
        salesQaLisp3: "Sarah Johnson",
        customerContact: "David Lee",
        printType: "OFFSETprint",
        printTypeName: "OFFSET",
        otherPrint: "",
        itemDescription: "Box for server components",
        materialType: "CORRUGATED",
        otherMaterial: "",
        sizeDimension: "InnerDimension",
        tolerance: "±1mm",
        printQty: 4,
        printTolerance: "±1.5mm",
        testRequirements: [
            { name: "BCT", value: "8.2kgf" },
            { name: "DROPTEST", value: "1.2m" }
        ],
        otherTests: [
            { name: "ROHS 2", value: "Compliant" }
        ],
        projectNature: ["Existing Item", "Revision"],
        revisionNumber: "2",
        productDescription: "Enhanced packaging for server components with additional cushioning for protection during transit.",
        illustrationNotes: "Updated logo placement according to new brand guidelines.",
        uploadedFiles: ["revised_specifications.pdf", "new_logo.png"],
        requestedBy: "Alex Wong",
        notedBy: "Patricia Garcia"
    },
    {
        id: "DRF-2024-003",
        dateSubmitted: "2024-02-27",
        type: "SAMPLEreq",
        typeName: "SAMPLE REQUEST",
        customer: "Global Manufacturing",
        partCode: "GM-456-Y",
        dateNeeded: "2024-03-05",
        status: "rejected",
        qty: 10,
        salesInCharge: "Thomas Rodriguez",
        salesJapanDesk: "Sato Yuki",
        salesQaLtc: "Lisa Park",
        salesQaLisp3: "Kevin Martinez",
        customerContact: "Jennifer Kim",
        printType: "NOprint",
        printTypeName: "NO PRINT",
        otherPrint: "",
        itemDescription: "Prototype packaging for new product line",
        materialType: "FOAM",
        otherMaterial: "",
        sizeDimension: "OuterDimension",
        tolerance: "±0.3mm",
        printQty: 0,
        printTolerance: "N/A",
        testRequirements: [
            { name: "DROPTEST", value: "1.5m" }
        ],
        otherTests: [],
        projectNature: ["New Item", "Customer Supplied Product"],
        revisionNumber: "",
        productDescription: "Sample packaging for prototype evaluation. Needs to be lightweight but provide adequate protection.",
        illustrationNotes: "",
        uploadedFiles: ["product_dimensions.pdf"],
        requestedBy: "Carlos Mendez",
        notedBy: "Michelle Taylor"
    }
];

// Function to populate the table with data
function populateTable(data) {
    const tableBody = document.getElementById('table-body');
    tableBody.innerHTML = '';

    data.forEach(request => {
        const row = document.createElement('tr');

        // Format the request type for display
        let requestTypeName = "";
        if (request.type === "IDFreq") requestTypeName = "IDF Request";
        else if (request.type === "FDFreq") requestTypeName = "FDF Request";
        else if (request.type === "SAMPLEreq") requestTypeName = "Sample Request";

        row.innerHTML = `
                    <td>${request.id}</td>
                    <td>${request.dateSubmitted}</td>
                    <td>${requestTypeName}</td>
                    <td>${request.customer}</td>
                    <td>${request.partCode}</td>
                    <td>${request.dateNeeded}</td>
                    <td class="status-${request.status}">${request.status.charAt(0).toUpperCase() + request.status.slice(1)}</td>
                    <td class="action-buttons">
                        <button class="btn btn-view" onclick="viewRequest('${request.id}')">View</button>
                        ${request.status === 'pending' ? `
                            <button class="btn btn-approve" onclick="updateRequestStatus('${request.id}', 'approved')">Approve</button>
                            <button class="btn btn-reject" onclick="updateRequestStatus('${request.id}', 'rejected')">Reject</button>
                        ` : ''}
                    </td>
                `;

        tableBody.appendChild(row);
    });
}

// Function to filter the table
function filterTable() {
    const searchInput = document.getElementById('search-input').value.toLowerCase();
    const typeFilter = document.getElementById('filter-type').value;
    const statusFilter = document.getElementById('filter-status').value;

    const filteredData = requestData.filter(request => {
        // Check if the request matches the search input
        const matchesSearch =
            request.id.toLowerCase().includes(searchInput) ||
            request.customer.toLowerCase().includes(searchInput) ||
            request.partCode.toLowerCase().includes(searchInput);

        // Check if the request matches the type filter
        const matchesType = typeFilter === '' || request.type === typeFilter;

        // Check if the request matches the status filter
        const matchesStatus = statusFilter === '' || request.status === statusFilter;

        return matchesSearch && matchesType && matchesStatus;
    });

    populateTable(filteredData);
}

// Function to reset filters
function resetFilters() {
    document.getElementById('search-input').value = '';
    document.getElementById('filter-type').value = '';
    document.getElementById('filter-status').value = '';
    populateTable(requestData);
}

// Function to view request details
function viewRequest(requestId) {
    const request = requestData.find(req => req.id === requestId);
    if (!request) return;

    // Populate the modal with request details
    document.getElementById('detail-request-id').textContent = request.id;
    document.getElementById('detail-request-type').textContent = request.typeName;
    document.getElementById('detail-date-submitted').textContent = request.dateSubmitted;
    document.getElementById('detail-date-needed').textContent = request.dateNeeded;
    document.getElementById('detail-qty').textContent = request.qty;

    const statusElement = document.getElementById('detail-status');
    statusElement.textContent = request.status.charAt(0).toUpperCase() + request.status.slice(1);
    statusElement.className = `status-${request.status}`;

    document.getElementById('detail-sales-in-charge').textContent = request.salesInCharge;
    document.getElementById('detail-sales-japan-desk').textContent = request.salesJapanDesk;
    document.getElementById('detail-sales-qa-ltc').textContent = request.salesQaLtc;
    document.getElementById('detail-sales-qa-lisp3').textContent = request.salesQaLisp3;
    document.getElementById('detail-customer-name').textContent = request.customer;
    document.getElementById('detail-customer-contact').textContent = request.customerContact;

    document.getElementById('detail-print-type').textContent = request.printTypeName;
    document.getElementById('detail-other-print').textContent = request.otherPrint || 'N/A';
    document.getElementById('detail-partcode').textContent = request.partCode;
    document.getElementById('detail-item-desc').textContent = request.itemDescription;
    document.getElementById('detail-material-type').textContent = request.materialType;
    document.getElementById('detail-other-material').textContent = request.otherMaterial || 'N/A';

    document.getElementById('detail-size').textContent =
        request.sizeDimension === 'InnerDimension' ? 'Inner Dimension' : 'Outer Dimension';
    document.getElementById('detail-tolerance').textContent = request.tolerance;
    document.getElementById('detail-print-qty').textContent = request.printQty;
    document.getElementById('detail-print-tolerance').textContent = request.printTolerance;

    // Format test requirements
    const testReqText = request.testRequirements.map(test => `${test.name}: ${test.value}`).join(', ');
    document.getElementById('detail-test-req').textContent = testReqText || 'None';

    const otherTestText = request.otherTests.map(test => `${test.name}: ${test.value}`).join(', ');
    document.getElementById('detail-other-test').textContent = otherTestText || 'None';

    document.getElementById('detail-project-nature').textContent = request.projectNature.join(', ');
    document.getElementById('detail-revision').textContent = request.revisionNumber || 'N/A';

    document.getElementById('detail-product-desc').textContent = request.productDescription;
    document.getElementById('detail-illustration-notes').textContent = request.illustrationNotes || 'None';

    // Format uploaded files
    const filesHTML = request.uploadedFiles.map(file => `<a href="#">${file}</a>`).join(', ');
    document.getElementById('detail-files').innerHTML = filesHTML || 'None';

    document.getElementById('detail-requested-by').textContent = request.requestedBy;
    document.getElementById('detail-noted-by').textContent = request.notedBy;

    // Show the modal
    document.getElementById('request-modal').style.display = 'block';
}

// Function to close the modal
function closeModal() {
    document.getElementById('request-modal').style.display = 'none';
}

// Function to update the status of a request
function updateRequestStatus(requestId, newStatus) {
    const request = requestData.find(req => req.id === requestId);
    if (request) {
        request.status = newStatus;
        populateTable(requestData);
        alert(`Request ${requestId} has been ${newStatus}.`);
    }
}

// Function to update status from the modal
function updateStatus(newStatus) {
    const requestId = document.getElementById('detail-request-id').textContent;
    updateRequestStatus(requestId, newStatus);
    closeModal();
}

// Function for pagination
function goToPage(pageNumber) {
    // This would typically fetch the appropriate page of data from the server
    // For this example, we'll just update the active button
    const paginationButtons = document.querySelectorAll('#pagination button');
    paginationButtons.forEach(button => {
        button.classList.remove('active');
        if (button.textContent == pageNumber) {
            button.classList.add('active');
        }
    });
}

// Function to export data
function exportData() {
    alert('Data would be exported to CSV/Excel here.');
    // In a real implementation, this would generate a CSV or Excel file
}

// Close the modal if the user clicks outside of it
window.onclick = function (event) {
    const modal = document.getElementById('request-modal');
    if (event.target === modal) {
        modal.style.display = 'none';
    }
}

// Initialize the table when the page loads
window.onload = function () {
    populateTable(requestData);
}