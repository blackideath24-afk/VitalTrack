document.addEventListener("DOMContentLoaded", function () {
    const feedback = document.querySelector('.feedback.success');
    if (feedback) {
        setTimeout(() => feedback.style.display = 'none', 4000);
    }
});