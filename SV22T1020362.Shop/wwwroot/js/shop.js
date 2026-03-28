// ===== TOAST NOTIFICATION =====
/**
 * Hiển thị thông báo toast ở góc dưới phải
 * @param {string} message - Nội dung thông báo
 * @param {'success'|'error'|'info'} type - Loại thông báo
 */
function showToast(message, type = 'success') {
    let container = document.getElementById('toastContainer');
    if (!container) {
        container = document.createElement('div');
        container.id = 'toastContainer';
        document.body.appendChild(container);
    }

    const toast = document.createElement('div');
    toast.className = `shop-toast ${type}`;
    toast.innerHTML = `<i class="bi bi-${type === 'success' ? 'check-circle' : 'exclamation-circle'} me-2"></i>${message}`;
    container.appendChild(toast);

    // Tự động xóa sau 3.5 giây
    setTimeout(() => {
        toast.style.opacity = '0';
        toast.style.transition = 'opacity 0.4s';
        setTimeout(() => toast.remove(), 400);
    }, 3500);
}

// ===== ADD TO CART (AJAX) =====
/**
 * Thêm sản phẩm vào giỏ hàng qua AJAX
 * @param {number} productID - Mã sản phẩm
 * @param {number} quantity  - Số lượng
 */
function addToCart(productID, quantity) {
    if (!quantity || quantity < 1) quantity = 1;

    const formData = new FormData();
    formData.append('productID', productID);
    formData.append('quantity', quantity);

    fetch('/Cart/AddToCart', { method: 'POST', body: formData })
        .then(r => r.json())
        .then(result => {
            if (result.success) {
                showToast(result.message, 'success');
                // Cập nhật badge giỏ hàng
                const badge = document.getElementById('cartBadge');
                if (badge) badge.textContent = result.cartCount;
            } else {
                showToast(result.message, 'error');
            }
        })
        .catch(() => showToast('Không thể thêm vào giỏ hàng', 'error'));
}

// ===== PRODUCT DETAIL - ĐỔI ẢNH KHI CLICK THUMBNAIL =====
/**
 * Đổi ảnh lớn khi click thumbnail
 * @param {string} src - Đường dẫn ảnh
 * @param {HTMLElement} el - Element thumbnail đang click
 */
function switchMainImage(src, el) {
    const mainImg = document.getElementById('mainProductImage');
    if (mainImg) mainImg.src = src;

    // Bỏ active tất cả thumbnail, thêm active cho cái đang click
    document.querySelectorAll('.thumb-img').forEach(t => t.classList.remove('active'));
    el.classList.add('active');
}

// ===== QUANTITY CONTROL =====
/**
 * Tăng/giảm số lượng trong ô input
 * @param {string} inputId - id của input số lượng
 * @param {number} delta   - +1 hoặc -1
 */
function changeQuantity(inputId, delta) {
    const input = document.getElementById(inputId);
    if (!input) return;
    let val = parseInt(input.value) || 1;
    val += delta;
    if (val < 1) val = 1;
    input.value = val;
}

// ===== FORMAT CURRENCY =====
/**
 * Format số thành chuỗi tiền tệ VN (dấu chấm ngăn cách hàng nghìn)
 * @param {number} amount
 * @returns {string}
 */
function formatCurrency(amount) {
    return amount.toLocaleString('vi-VN') + ' đ';
}

// ===== CART: CẬP NHẬT TỔNG TIỀN KHI THAY ĐỔI SỐ LƯỢNG =====
document.addEventListener('DOMContentLoaded', function () {
    // Tự động tính lại tổng khi thay đổi số lượng trong giỏ hàng
    document.querySelectorAll('.cart-qty-input').forEach(function (input) {
        input.addEventListener('change', function () {
            updateCartRowTotal(this);
        });
    });
});

/**
 * Cập nhật thành tiền của một dòng trong giỏ hàng
 * @param {HTMLInputElement} input - Ô nhập số lượng
 */
function updateCartRowTotal(input) {
    const row = input.closest('tr');
    if (!row) return;
    const price = parseFloat(row.dataset.price || 0);
    const qty = parseInt(input.value) || 1;
    const total = price * qty;

    const totalCell = row.querySelector('.row-total');
    if (totalCell) totalCell.textContent = total.toLocaleString('vi-VN') + ' đ';

    // Cập nhật tổng toàn bộ giỏ
    recalcCartTotal();
}

/**
 * Tính lại tổng tiền toàn bộ giỏ hàng
 */
function recalcCartTotal() {
    let grand = 0;
    document.querySelectorAll('.cart-qty-input').forEach(function (input) {
        const row = input.closest('tr');
        if (!row) return;
        const price = parseFloat(row.dataset.price || 0);
        const qty = parseInt(input.value) || 1;
        grand += price * qty;
    });
    const grandEl = document.getElementById('cartGrandTotal');
    if (grandEl) grandEl.textContent = grand.toLocaleString('vi-VN') + ' đ';
}