let otpSeconds = 8;
let interval = null;

function startOtpTimer() {
    const timer = document.getElementById("otpTimer");
    const resend = document.getElementById("resendCode");

    if (!timer || !resend)
        return;

    otpSeconds = 8;

    timer.style.display = "block";
    resend.style.display = "none";

    if (interval) {
        clearInterval(interval);
    }

    updateTimer();

    interval = setInterval(function () {
        otpSeconds--;

        updateTimer();

        if (otpSeconds <= 0) {
            clearInterval(interval);
            interval = null;

            timer.style.display = "none";
            resend.style.display = "inline-block";
        }
    }, 1000);

    function updateTimer() {
        const minutes = Math.floor(otpSeconds / 60);
        const seconds = otpSeconds % 60;

        timer.innerHTML =
            `${minutes}:${seconds.toString().padStart(2, "0")}`;
    }
}