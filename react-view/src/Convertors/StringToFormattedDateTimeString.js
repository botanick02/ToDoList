export default function StringToFormattedDateTimeString(string) {
    let date = new Date(string);
    let stringDate = date.toLocaleString();
    return stringDate === 'Invalid Date' ? '' : stringDate;
}