import React from 'react';
import Button from 'retail-ui/components/Button'
import Checkbox from 'retail-ui/components/Checkbox'
import { CaseSuite, Case } from '../Case';

export default class CheckboxTestPage extends React.Component {
    state = {
        value: null,
        value2: null,
    };

    render(): React.Element<*> {
        return (
            <CaseSuite title='Checkbox'>
                <Case title='Simple Checkbox'>
                    <Case.Body>
                        <Checkbox
                            data-tid='SimpleCheckbox'
                            checked={this.state.value} 
                            onChange={(e, value) => this.setState({ value: value })}
                        />
                    </Case.Body>
                </Case>
                <Case title='Checkbox with label'>
                    <Case.Body>
                        <Checkbox
                            data-tid='CheckboxWithLabel'
                            checked={this.state.value2} 
                            onChange={(e, value) => this.setState({ value2: value })}
                        >
                            Checkbox label
                        </Checkbox>
                    </Case.Body>
                </Case>
           </CaseSuite>
        );
    }
}