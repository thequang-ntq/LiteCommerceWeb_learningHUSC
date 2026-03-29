// ===== TOAST NOTIFICATION =====
/**
 * Hiển thị thông báo toast
 * @param {string} message - Nội dung thông báo
 * @param {string} type - Loại: 'success' | 'error' | 'info'
 */
function showToast(message, type) {
    // Xóa toast cũ nếu có
    const old = document.getElementById('shopToast');
    if (old) old.remove();

    const bgClass = type === 'success' ? 'bg-success'
        : type === 'error' ? 'bg-danger'
            : 'bg-info';

    const toast = document.createElement('div');
    toast.id = 'shopToast';
    toast.innerHTML = `
        <div class="toast align-items-center text-white ${bgClass} border-0 show"
             role="alert" aria-live="assertive"
             style="min-width:280px;">
            <div class="d-flex">
                <div class="toast-body">
                    <i class="bi bi-${type === 'success' ? 'check-circle' : type === 'error' ? 'x-circle' : 'info-circle'} me-2"></i>
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto"
                        onclick="this.closest('#shopToast').remove()"></button>
            </div>
        </div>`;

    toast.style.cssText = 'position:fixed;bottom:20px;right:20px;z-index:9999;';
    document.body.appendChild(toast);

    // Tự ẩn sau 3 giây
    setTimeout(() => { if (toast.parentNode) toast.remove(); }, 3000);
}

// ===== ADD TO CART (AJAX) =====
/**
 * Thêm sản phẩm vào giỏ hàng qua AJAX
 * @param {number} productID - Mã sản phẩm
 * @param {number} quantity - Số lượng
 */
function addToCart(productID, quantity) {
    if (!quantity || quantity < 1) quantity = 1;

    const formData = new FormData();
    formData.append('productID', productID);
    formData.append('quantity', quantity);

    fetch('/Cart/AddToCart', {
        method: 'POST',
        body: formData
    })
        .then(r => r.json())
        .then(result => {
            if (result.success) {
                // Cập nhật badge giỏ hàng
                const badge = document.getElementById('cartBadge');
                if (badge && result.cartCount !== undefined) {
                    badge.textContent = result.cartCount;
                }
                showToast(result.message || 'Đã thêm vào giỏ hàng', 'success');
            } else {
                showToast(result.message || 'Có lỗi xảy ra', 'error');
            }
        })
        .catch(() => showToast('Không thể kết nối máy chủ', 'error'));
}

// ===== PRODUCT DETAIL - ĐỔI ẢNH KHI CLICK THUMBNAIL =====
/**
 * Chuyển ảnh chính trong trang chi tiết sản phẩm
 * @param {string} src - Đường dẫn ảnh
 * @param {HTMLElement} thumbEl - Phần tử thumbnail được click
 */
function switchMainImage(src, thumbEl) {
    const main = document.getElementById('mainProductImage');
    if (main) main.src = src;

    // Bỏ active tất cả thumbnail
    document.querySelectorAll('.thumb-img').forEach(el => el.classList.remove('active'));
    if (thumbEl) thumbEl.classList.add('active');
}

// ===== QUANTITY CONTROL =====
/**
 * Thay đổi số lượng trong input số
 * @param {string} inputId - ID của input
 * @param {number} delta - Thay đổi (+1 hoặc -1)
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