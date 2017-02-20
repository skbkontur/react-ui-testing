import React from 'react';
import ReactDOM from 'react-dom';
import styled from 'styled-components';
import './reset.less';

const ContentWrapper = styled.div`
    min-height: 100vh;
    background-color: #eee;
    padding: 40px;
    box-sizing: border-box;
    font-family: 'Segoe UI', 'Helvetica', 'Arial', 'Tahoma', sans-serif;
    font-size: 16px;
    line-height: 25px;
`;

const Content = styled.div`
    max-width: 800px;
    margin: 0 auto;
    background-color: #fff;
    padding: 40px;
    box-shadow: 
        0 1px 8px 0 rgba(0,0,0,.1), 
        0 1px 1px 0 rgba(0,0,0,.08);
`;

const Bookmarklet = styled.a`
    display: inlint-block;
    padding: 2px 10px;
    color: #333;
    text-decoration: none;    
    border: 1px solid #aaa;  
`;

class BookmarkletsPage extends React.Component {
    createHrefToBookmarklet(filename) {
        return (
            'javascript: (function () { ' + 
                'var jsCode = document.createElement("script"); ' + 
                `jsCode.setAttribute("src", "${process.env.bookmarkletsRoot}/${filename}"); ` + 
                'document.body.appendChild(jsCode); ' + 
            '}())'
        );
    }

    render(): React.Element<*> {
        return (
            <ContentWrapper>
                <Content>
                    <Bookmarklet
                        href={this.createHrefToBookmarklet('highlight-tid-bookmarklet.js')}>
                        Highlight tids
                    </Bookmarklet>
                </Content>
            </ContentWrapper>
        );
    }
}

ReactDOM.render(
    <BookmarkletsPage />,
    document.getElementById('content')
);