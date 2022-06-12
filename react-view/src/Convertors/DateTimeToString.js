export default function DateTimeToString(date) {
    let stringDate = date.getFullYear() + "-" + (To2digit(date.getMonth() + 1)) +
        "-" + To2digit(date.getDate()) + "T" + To2digit(date.getHours()) + ":" + To2digit(date.getMinutes());
    return stringDate === 'Invalid Date' ? '' : stringDate;
}

function To2digit(num) {
    if (num >= 0 && num <= 9) {
        return '0' + num;
    }
    return num;
}