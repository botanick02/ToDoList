export default function StringToFormattedDateTimeString(inputDate: string | null): string {
    if(!inputDate){
        return "";
    }
    let date = new Date(inputDate);
    let stringDate = date.toLocaleString();
    return stringDate === 'Invalid Date' ? '' : stringDate;
}