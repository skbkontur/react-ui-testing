global.ReactSeleniumTesting = {
    attributeWhiteList: {
        'error': [/.*/],
        'disabled': [/.*/],
        'disablePortal': ['ComboBoxRenderer'],
        'checked': [/.*/],
        'items': ['RadioGroup'],
        'value': [/.*/],
        'customProp1': ['SomeComponent'],
        'customProp2': [/^.+SomeComponent.+$/],
    },
    acceptAttribute: (prevAcceptResult, componentName, propName) => {
        if (componentName === 'Select' && propName === 'items') {
            return true;
        }
        return prevAcceptResult;
    }
};
