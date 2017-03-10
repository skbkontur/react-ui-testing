import React from "react"
import ReactDom from "react-dom"
import TestPage from './ReactTestApplication';

ReactDom.render(
    <TestPage data-tid="testpage" />,
    document.getElementById('content')
);
