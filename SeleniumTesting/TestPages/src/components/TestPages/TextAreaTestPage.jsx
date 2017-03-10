import React from 'react';
import Button from 'retail-ui/components/Button'
import Textarea from 'retail-ui/components/Textarea'
import { CaseSuite, Case } from '../Case';

export default class TextareaTestPage extends React.Component {
    state = {
        value: null,
    };

    render(): React.Element<*> {
        return (
            <CaseSuite title='Textarea'>
                <Case title='Simple Textarea'>
                    <Case.Body>
                        <Textarea
                            data-tid='SimpleTextarea'
                            value={this.state.value} 
                            onChange={(e, value) => this.setState({ value: value })}
                        />
                    </Case.Body>
                </Case>
           </CaseSuite>
        );
    }
}