import moment from "moment";
import MEnum from "@/script/enum";
import numeral from "numeral";

export const filterDate = (value) => {
  if (!value) return "";
  return moment(value).format("DD/MM/YYYY");
};

export const filterGender = (value) => {
    switch (value) {
      case MEnum.GENDER.MALE:
        return "Nam";
      case MEnum.GENDER.FEMALE:
        return "Nữ";
      default:
        return "Khác";
    }
};

export const formatSpaceNumber = (number) => {
    if (number){
        return numeral(number).format("0,0");
    }
    return "";
}

export const filterDateFormat = (value, format) => {
  if (!value) return "";
  
  return moment(value).format(format);
}
