export const isEqualDeep = (val1, val2) => {
    return JSON.stringify(val1) === JSON.stringify(val2);
};

export const hasValue = val => {
    return val !== null && val !== undefined
}
export const isEmptyObject = (obj) => {
    for (var key in obj) {
      // eslint-disable-next-line no-prototype-builtins
      if (obj.hasOwnProperty(key)) {
        return false;
      }
    }
    return true;
}

export const downloadFile = (res, fileName) => {
  const url = window.URL.createObjectURL(new Blob([res.data]));
  const link = document.createElement("a");
  link.href = url;
  // const disposition = res.headers["content-disposition"];

  // if (disposition && disposition.includes("attachment")) {
  //   const filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
  //   const matches = filenameRegex.exec(disposition);
  //   if (matches != null && matches[1]) {
  //     filename = matches[1].replace(/['"]/g, "");
  //   }
  // }

  link.setAttribute("download", fileName);
  document.body.appendChild(link);

  link.click();
  document.body.removeChild(link);
};
