// ClickOutsideDirective.js
export  const clickOutsideDirective = {
    mounted(el, binding) {
      // Define a click event handler
      const onClickOutside = (event) => {
        // Check if the clicked element is outside the target element
        if (!(el === event.target || el.contains(event.target))) {
          // Call the provided method or emit the specified event
          binding.value();
        }
      };
  
      // Attach the event listener to the whole document
      document.addEventListener('click', onClickOutside);
  
      // Cleanup the event listener when the component is unmounted
      el._clickOutside = onClickOutside;
    },
    beforeUnmount(el) {
      // Remove the event listener when the component is unmounted
      document.removeEventListener('click', el._clickOutside);
      delete el._clickOutside;
    },
  };
  