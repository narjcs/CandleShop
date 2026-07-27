//let otpSeconds = 120;
let otpSeconds = 8;
let interval = null;

function startOtpTimer() {

    const timer = document.getElementById("otpTimer");
    const resend = document.getElementById("resendCode");

    if (!timer) return;

    otpSeconds = 8;
    // otpSeconds = 120;

    timer.style.display = "block";
    resend.style.display = "none";

    if (interval)
        clearInterval(interval);

    updateTimer();

    interval = setInterval(function () {

        otpSeconds--;

        updateTimer();

        if (otpSeconds <= 0) {

            clearInterval(interval);

            timer.style.display = "none";

            resend.style.display = "inline-block";
        }

    }, 1000);

    function updateTimer() {

        let min = Math.floor(otpSeconds / 60);
        let sec = otpSeconds % 60;

        timer.innerHTML =
            `ارسال مجدد کد تا <b>${min}:${sec.toString().padStart(2, '0')}</b>`;
    }
}

document.addEventListener("DOMContentLoaded", function () {

    const resend = document.getElementById("resendCode");

    if (!resend) return;

    resend.addEventListener("click", function (e) {

        e.preventDefault();

        startOtpTimer();

    });

});